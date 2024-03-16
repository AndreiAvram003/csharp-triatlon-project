using Triatlon_Project_C.Domain;

namespace Triatlon_Project_C.Repository;

public interface RefereeRepository : Repository<Referee, long>
{
    Referee Save(Referee referee);

    Referee GetById(long id);

    Referee Update(Referee referee);

    Referee DeleteById(long id);

    List<Referee> GetAll();
    
}