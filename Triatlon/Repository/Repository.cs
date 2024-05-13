using Triatlon.Domain;

namespace Triatlon.Repository;

public interface Repository<T,ID> where T: Entity<ID>
{
    T Save(T obj);
    T GetById(ID id);
    T Update(T obj);
    T DeleteById(ID id);

    List<T> GetAll();
}