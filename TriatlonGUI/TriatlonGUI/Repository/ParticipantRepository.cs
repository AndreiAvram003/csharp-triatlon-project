using TriatlonGUI.Domain;

namespace TriatlonGUI.Repository;

public interface ParticipantRepository : Repository<Participant, long>
{
    List<Participant> GetAllSorted();
}