using System.Data;
using Model.Domain;

namespace Repository.Repository;

public class RefereeDBRepo : RefereeRepository
{

    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    IDictionary<String, string> props;

    public RefereeDBRepo(IDictionary<String, string> props)
    {
        log.Info("Creating RefereeDBRepo ");
        this.props = props;
    }

    public Referee Save(Referee obj)
    {
        throw new NotImplementedException();
    }

    public Referee GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Referee Update(Referee obj)
    {
        throw new NotImplementedException();
    }

    public Referee DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public Referee FindByNameAndPassword(string name, string password)
    {
        IDbConnection con = BDUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT referees.id, referees.name, referees.password, referees.trial_id, trials.name AS trial_name FROM referees INNER JOIN trials ON referees.trial_id = trials.id WHERE name = @name AND password = @password";
                ;
                var param = comm.CreateParameter();
                param.ParameterName = "@name";
                param.Value = name;
                comm.Parameters.Add(param);

                param = comm.CreateParameter();
                param.ParameterName = "@password";
                param.Value = password;
                comm.Parameters.Add(param);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        long id = dataR.GetInt64(0);
                        String refereeName = dataR.GetString(1);
                        String refereePassword = dataR.GetString(2);
                        long trialId = dataR.GetInt64(3);
                        String trialName = dataR.GetString(4);
                        List<Participant> participants = GetParticipantsForTrialRef(trialId);
                        Trial trial = new Trial(trialId, null, participants, trialName);
                        Referee referee = new Referee(id, refereeName, refereePassword, trial);
                        trial.referee = referee;
                        return referee;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching referee by name and password: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }

        return null;
    }


public List<Referee> GetAll()
    {
        log.Info("Entering findAll referees");
        List<Referee> referees = new List<Referee>();
        IDbConnection con = BDUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText =
                    "SELECT referees.id, referees.name, referees.password, referees.trial_id, trials.name AS trial_name FROM referees INNER JOIN trials ON referees.trial_id = trials.id";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long id = dataR.GetInt64(0);
                        String name = dataR.GetString(1);
                        String password = dataR.GetString(2);
                        long trialId = dataR.GetInt64(3);
                        String trialName = dataR.GetString(4);
                        List<Participant> participants = GetParticipantsForTrialRef(trialId);

                        Trial trial = new Trial(trialId, null, participants, trialName);
                        Referee referee = new Referee(id, name, password, trial);
                        trial.referee = referee;
                        referees.Add(referee);
                    }

                    return referees;
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching referees: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }
    }


    private List<Participant> GetParticipantsForTrialRef(long trialId)
    {
        List<Participant> participants = new List<Participant>();
        IDbConnection con = BDUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT p.* FROM participants p " +
                                   "INNER JOIN results res ON p.id = res.participant_id " +
                                   "WHERE res.trial_id = @trialId";
                var param = comm.CreateParameter();
                param.ParameterName = "@trialId";
                param.Value = trialId;
                comm.Parameters.Add(param);

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long id = dataR.GetInt64(0);
                        String name = dataR.GetString(1);
                        int points = dataR.GetInt32(2);
                        Participant participant = new Participant(id, name, points);
                        participants.Add(participant);
                    }
                }
            }

            return participants;
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching participants for trial: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }
    }
}