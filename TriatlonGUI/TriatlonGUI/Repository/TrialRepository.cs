using TriatlonGUI.Domain;

namespace TriatlonGUI.Repository;

public interface TrialRepository : Repository<Trial,long>
{
   List<Participant> GetParticipantsForTrialRef(long trialId);
}