using System.Data;
using System.Text.RegularExpressions;
using log4net;
using TriatlonGUI;
using TriatlonGUI.Domain;
using TriatlonGUI.Repository;

namespace DefaultNamespace;

public class ParticipantDBRepo : ParticipantRepository


{
    
    private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    IDictionary<String, string> props;
    public ParticipantDBRepo(IDictionary<String, string> props)
    {
        log.Info("Creating ParticipantDBRepo ");
        this.props = props;
    }
    
    
    public Participant Save(Participant participant)
    {
        throw new System.NotImplementedException();
    }

    public Participant GetById(long id)
    {
        throw new System.NotImplementedException();
    }

    public Participant Update(Participant participant)
    {
        throw new System.NotImplementedException();
    }

    public Participant DeleteById(long id)
    {
        throw new System.NotImplementedException();
    }

    public List<Participant> GetAll()
    {
        log.Info("Entering findAll participants");
        IDbConnection con = BDUtils.getConnection(props);
        List<Participant> participants = new List<Participant>();
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM participants";

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

            log.InfoFormat("findAll found {0} participants", participants.Count());
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching participants: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }

        return participants;
    }

    public List<Participant> GetAllSorted()
    {
        log.Info("Entering findAll participants");
        List<Participant> participants = new List<Participant>();
        IDbConnection con = BDUtils.getConnection(props);
        try
        {
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "SELECT * FROM participants ORDER BY name ASC";

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
            
            log.InfoFormat("findAll found {0} participants", participants.Count);
        }
        catch (Exception ex)
        {
            log.Error("Error while fetching participants: " + ex.Message);
            throw; // Rethrow the exception to the calling code
        }
        
        return participants;
    }
}
    
