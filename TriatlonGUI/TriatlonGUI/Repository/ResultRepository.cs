using TriatlonGUI.Domain;

namespace TriatlonGUI.Repository;

public interface ResultRepository : Repository<Result,long>
{
    int GetTotalPointsAtTrial(long participantId, long trialId);
}