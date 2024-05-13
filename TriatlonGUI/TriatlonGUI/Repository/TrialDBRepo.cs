using System.Data;
using TriatlonGUI.Domain;

namespace TriatlonGUI.Repository;

public class TrialDBRepo:TrialRepository
{
    
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    
    IDictionary<String, string> props;
    
    public TrialDBRepo(IDictionary<String, string> props)
    {
        log.Info("Creating TrialDBRepo ");
        this.props = props;
    }
    
    
    public Trial Save(Trial obj)
    {
        throw new NotImplementedException();
    }

    public Trial GetById(long id1)
    {
        throw new NotImplementedException();
    }

    public Trial Update(Trial obj)
    {
        throw new NotImplementedException();
    }

    public Trial DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public List<Trial> GetAll()
    {
        log.Info("Entering findAll referees");
        List<Trial> trials = new List<Trial>();
        IDbConnection con = BDUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                string query =
                    "SELECT trials.id, trials.name, trials.referee_id, referees.name AS ref_name,referees.password as ref_password FROM trials INNER JOIN referees ON trials.referee_id = referees.id";

                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        long id = dataR.GetInt64(0);
                        String name = dataR.GetString(1);
                        long refereeId = dataR.GetInt64(2);
                        String refereeName = dataR.GetString(3);
                        String refereePassword = dataR.GetString(4);
                        List<Participant> participants = GetParticipantsForTrialRef(id);

                        Referee referee = new Referee(refereeId, refereeName, refereePassword, null);
                        Trial trial = new Trial(id, null, participants, name);
                        referee.trial = trial;
                        trials.Add(trial);
                    }

                    return trials;
                }
            }
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching referees: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }
    }
    public List<Participant> GetParticipantsForTrialRef(long trialId)
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