using Triatlon_Project_C.Domain;

namespace Triatlon_Project_C.Repository;

public interface Repository<T,ID> where T: Entity<ID>
{
    T Save(T obj);
    T GetById(ID id);
    T Update(T obj);
    T DeleteById(ID id);

    List<T> GetAll();
}