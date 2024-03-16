namespace Triatlon_Project_C.Domain;

public class Trial : Entity<long>
{

    public Referee referee{get;set;}
    public List<Participant> participants{get;set;}

    public Trial(Referee referee, List<Participant> participants) : base(0)
    {
        this.referee = referee;
        this.participants = participants;
    }

  }