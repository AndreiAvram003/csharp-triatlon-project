using Model.Domain;
using Repository.Repository;
using Service.Service;

namespace Server;

public class ServiceImpl:IService
{
    private RefereeRepository refereeRepository;
    private ParticipantRepository participantRepository;
    private ResultRepository resultRepository;
    private TrialRepository trialRepository;

    private Dictionary<long, IRefereeObserver> loggedReferees;

    public ServiceImpl(ParticipantRepository participantRepository,TrialRepository trialRepository,ResultRepository resultRepository,RefereeRepository refereeRepository)
    {
        this.refereeRepository = refereeRepository;
        this.participantRepository = participantRepository;
        this.resultRepository = resultRepository;
        this.trialRepository = trialRepository;
        loggedReferees = new Dictionary<long, IRefereeObserver>();
    }

    public Referee Login(string username, string password,IRefereeObserver client)
    {
        List<Referee> referees = refereeRepository.GetAll().ToList();
        Referee referee = referees.Find(r => r.name.Equals(username) && r.password.Equals(password));
        if (referee != null)
        {
            if (loggedReferees.ContainsKey(referee.id))
            {
                throw new Exception("Referee already logged in");
            }
            loggedReferees.Add(referee.id, client);
            return referee;
        }
        return null;
    }

    public void Logout(String username, String password,IRefereeObserver client)
    {
        Referee referee = refereeRepository.FindByNameAndPassword(username, password);
        Console.WriteLine(referee.id);
        loggedReferees.Remove(referee.id);
    }
    

    public List<Participant> GetParticipants(Referee referee)
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
        return resultRepository.GetTotalPointsAtTrial(participant.id, trial.id);
    }

    public List<Participant> GetParticipantsAtTrial(Trial trial)
    {
        return trialRepository.GetParticipantsForTrialRef(trial.id).ToList();
    }
}