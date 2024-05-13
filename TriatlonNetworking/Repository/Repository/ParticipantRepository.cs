using Model.Domain;


namespace Repository.Repository;

public interface ParticipantRepository : Repository<Participant, long>
{
    List<Participant> GetAllSorted();
}