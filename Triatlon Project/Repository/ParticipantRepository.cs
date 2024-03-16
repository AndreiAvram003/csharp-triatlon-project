using Triatlon_Project_C.Domain;

namespace Triatlon_Project_C.Repository;

public interface ParticipantRepository : Repository<Participant, long>
{
    Participant Save(Participant participant);

    Participant GetById(long id);

    Participant Update(Participant participant);

    Participant DeleteById(long id);

    List<Participant> GetAll();
    
}
