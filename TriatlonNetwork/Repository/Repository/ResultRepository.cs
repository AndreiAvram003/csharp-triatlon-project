using Model.Domain;

namespace Repository.Repository;

public interface ResultRepository : Repository<Result,long>
{
    int GetTotalPointsAtTrial(long participantId, long trialId);
}