using System.Data;
using Microsoft.EntityFrameworkCore;
using Model.Domain;
using Repository.connectionUtils;

namespace Repository.Repository;

public class TrialDBRepo:TrialRepository
{
    
    private readonly TriatlonDBContext _context;

    public TrialDBRepo(TriatlonDBContext context)
    {
        _context = context;
    }


    public Trial Save(Trial obj)
    {
        throw new NotImplementedException();
    }

    public Trial GetById(long id)
    {
        return _context.Trials
            .Include(t => t.Results)
            .ThenInclude(res => res.participant)
            .FirstOrDefault(t => t.id == id);
    }

    public Trial Update(Trial obj)
    {
        throw new NotImplementedException();
    }

    public Trial DeleteById(long id)
    {
        throw new NotImplementedException();
    }

    public List<Trial> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<Participant> GetParticipantsForTrialRef(long trialId)
    {
        throw new NotImplementedException();
    }
}