using System.Data;
using System.Text.RegularExpressions;
using log4net;
using Triatlon_Project_C.Domain;
using Triatlon_Project_C.Repository;

namespace DefaultNamespace;

public class ParticipantDBRepo : Repository<Participant,long>


{
    
    private static readonly ILog log = LogManager.GetLogger("ParticipantDBRepo");

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
        throw new System.NotImplementedException();
    }

    public List<Participant> GetAllSorted()
    {
        log.Info("Entering findAll participants");
        IDbConnection con = DBUtils.getConnection(props);
        List<Participant> participants = new List<Participant>();
        using (var comm = con.CreateCommand())
        {
            comm.CommandText = "SELECT * FROM participants ORDER BY name DESC";

            using (var dataR = comm.ExecuteReader())
            {
                while (dataR.Read())
                {
                    long id = dataR.GetInt64(0);
                    String name = dataR.GetString(1);
                    int points = dataR.GetInt32(2);
                    Participant participant = new Participant(name, points);
                    participant.id = id;
                    participants.Add(participant);
                }
            }
        }
			
        log.InfoFormat("findAll found {0} participants",participants.Count());
        return participants;
    }
}
    
