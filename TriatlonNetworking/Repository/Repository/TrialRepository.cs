using Model.Domain;

namespace Repository.Repository;

public interface TrialRepository : Repository<Trial,long>
{
   List<Participant> GetParticipantsForTrialRef(long trialId);
}