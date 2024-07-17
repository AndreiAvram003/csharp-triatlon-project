using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model.Domain
{
    [Table("referees")]
    public class Referee : Entity<long>
    {
        public Referee(long id, string name, string password, Trial trial, long trial_id) : base(id)
        {
            this.name = name;
            this.password = password;
            this.trial = trial;
            this.TrialId = trial_id;
        }

        public Referee() : base(0)
        {
        }

        public string name { get; set; }
        public string password { get; set; }

        [JsonIgnore]
        public Trial trial { get; set; }

        [Column("trial_id")]
        public long TrialId { get; set; }
    }
}