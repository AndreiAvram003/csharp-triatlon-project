using Triatlon.Domain;

namespace Triatlon.Service;

public interface Service
{
    Referee Login(string username, string password);
    void Logout();
    List<Participant> GetParticipants();
    Result AddResult(Participant participant, Trial trial, int points);
    int GetTotalPointsAtTrial(Participant participant, Trial trial);
    List<Participant> GetParticipantsWithPointsAtTrial(Trial trial);
}