using Triatlon_Project_C.Domain;

namespace Triatlon_Project_C.Repository;

public interface TrialRepository : Repository<Trial, long>
{
    Trial Save(Trial trial);

    Trial GetById(long id);

    Trial Update(Trial trial);

    Trial DeleteById(long id);

    List<Trial> GetAll();
}