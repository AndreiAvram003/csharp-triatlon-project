using Microsoft.EntityFrameworkCore;
using Model.Domain;
using System.Linq;
using Repository.connectionUtils;

namespace Repository.Repository
{
    public class RefereeDBRepo : RefereeRepository
    {
        private readonly TriatlonDBContext _context;

        public RefereeDBRepo(TriatlonDBContext context)
        {
            _context = context;
        }

        public Referee Save(Referee obj)
        {
            var referee = _context.Referees.Add(obj).Entity;
            _context.SaveChanges();
            return referee;
        }

        public Referee GetById(long id)
        {
            return _context.Referees
                .Include(r => r.trial)
                .ThenInclude(t => t.Results)
                .ThenInclude(res => res.participant)
                .FirstOrDefault(r => r.id == id);
        }

        public Referee Update(Referee obj)
        {
            var existingReferee = _context.Referees.Find(obj.id);
            if (existingReferee == null)
            {
                return null;
            }
            _context.Entry(existingReferee).CurrentValues.SetValues(obj);
            _context.SaveChanges();
            return existingReferee;
        }

        public Referee DeleteById(long id)
        {
            var referee = _context.Referees.Find(id);
            if (referee == null)
            {
                return null;
            }
            _context.Referees.Remove(referee);
            _context.SaveChanges();
            return referee;
        }

        public List<Referee> GetAll()
        {
            return _context.Referees
                .Include(r => r.trial)
                .ThenInclude(t => t.Results)
                .ThenInclude(res => res.participant)
                .ToList();
        }
    }
}