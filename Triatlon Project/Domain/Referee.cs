namespace Triatlon_Project_C.Domain;

public class Referee : Entity<long>
{
    public string name{get;set;}
    public string password{get; set; }
    public Trial trial{get; set; }

    public Referee(string name, string password, Trial trial) : base(0)
    {
        this.name = name;
        this.password = password;
        this.trial = trial;
    }

}