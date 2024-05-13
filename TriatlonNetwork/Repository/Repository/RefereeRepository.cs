using Model.Domain;

namespace Repository.Repository;

public interface RefereeRepository : Repository<Referee,long>
{
    Referee FindByNameAndPassword(string name, string password);
}