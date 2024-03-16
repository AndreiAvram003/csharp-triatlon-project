namespace Triatlon_Project_C.Domain;

public class Result : Entity<long>
{
    private Participant participant{get;set;}
    private Trial trial{get; set; }
	public  int result{get; set; }

    public Result(Participant participant, Trial trial) : base(0)
    {
        this.participant = participant;
        this.trial = trial;
		this.result = result;
    }

}