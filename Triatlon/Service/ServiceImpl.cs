using Triatlon.Domain;
using Triatlon.Repository;

namespace Triatlon.Service;

public class ServiceImpl:Service
{
    private RefereeRepository refereeRepository;
    private ParticipantRepository participantRepository;
    private ResultRepository resultRepository;
    private TrialRepository trialRepository;

    public ServiceImpl(ParticipantRepository participantRepository,TrialRepository trialRepository,ResultRepository resultRepository,RefereeRepository refereeRepository)
    {
        this.refereeRepository = refereeRepository;
        this.participantRepository = participantRepository;
        this.resultRepository = resultRepository;
        this.trialRepository = trialRepository;
    }

    public Referee Login(string username, string password)
    {
        List<Referee> referees = refereeRepository.GetAll().ToList();
        Referee referee = referees.Find(r => r.name.Equals(username) && r.password.Equals(password));
        if (referee != null)
        {
            return referee;
        }
        return null;
    }

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public List<Participant> GetParticipants()
    {
        return participantRepository.GetAllSorted().ToList();
    }

    public Result AddResult(Participant participant, Trial trial, int points)
    {
        Result result  = new Result(participant, trial, points);
        if (resultRepository.Save(result) != null)
        {
            return result;
        }

        return null;
    }

    public int GetTotalPointsAtTrial(Participant participant, Trial trial)
    {
        throw new NotImplementedException();
    }

    public List<Participant> GetParticipantsWithPointsAtTrial(Trial trial)
    {
        throw new NotImplementedException();
    }
}