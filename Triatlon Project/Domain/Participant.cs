namespace Triatlon_Project_C.Domain;

public class Participant : Entity<long>
{
    public string name { get; set; }
    public int points { get; set; }

    public Participant(string name, int points) : base(0)
    {
        this.name = name;
        this.points = points;
    }
}

   