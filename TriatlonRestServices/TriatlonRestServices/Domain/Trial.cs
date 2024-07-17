using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Domain;

[Table("trials")]

public class Trial : Entity<long>
{
    public Trial(long id, Referee referee,string name,long referee_id) : base(id)
    {
        this.referee = referee;
        this.name = name;
        this.referee_id = referee_id;
    }
    
    public Trial() : base(0)
    {
        
    }
    
    public Referee referee{get;set;}
    
    public string name{get;set;}
    
    [Column("referee_id")]
    public long referee_id{get;set;}
    
    public List<Result> Results { get; set; } = new List<Result>();

  }