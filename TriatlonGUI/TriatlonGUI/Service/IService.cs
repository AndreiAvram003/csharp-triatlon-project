using TriatlonGUI.Domain;

namespace TriatlonGUI.Service;

public interface IService
{
    Referee Login(string username, string password);
    void Logout();
    List<Participant> GetParticipants();
    Result AddResult(Participant participant, Trial trial, int points);
    int GetTotalPointsAtTrial(Participant participant, Trial trial);
    List<Participant> GetParticipantsAtTrial(Trial trial);
}