using System.Data;
using System.Text.RegularExpressions;
using log4net;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Repository;
using Repository.Repository;

namespace Repository.Repository;

public class ParticipantDBRepoEF : ParticipantRepository


{
    
    private readonly DbContext _dbContext;
    protected readonly DbSet<Participant> _dbSet;

    public ParticipantDBRepoEF(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<Participant>();
    }
    
    
    public List<Participant> GetAllSorted()
    {
        return _dbSet.OrderBy(p => p.name).ToList();
    }

    public Participant Save(Participant obj)
    {
        throw new NotImplementedException();
    }

    public Participant GetById(long id)
    {
        throw new NotImplementedException();
    }

    public Participant Update(Participant obj)
    {
        throw new NotImplementedException();
    }

    public Participant DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public List<Participant> GetAll()
    {
        return _dbSet.ToList();
    }
}