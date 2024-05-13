using Triatlon.Domain;

namespace Triatlon.Repository;

public interface ParticipantRepository : Repository<Participant, long>
{
    List<Participant> GetAllSorted();
}