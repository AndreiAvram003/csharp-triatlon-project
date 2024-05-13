namespace Model.Domain;

public class Participant : Entity<long>
{
    public Participant(long id, string name, int points) : base(id)
    {
        this.name = name;
        this.points = points;
    }

    public string name { get; set; }
    public int points { get; set; }
    
    
    public override string ToString()
    {
        return $"Participant: {name}, Points: {points}";
    }
}



   