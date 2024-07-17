using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Domain
{
    public class Result : Entity<long>
    {
        public Result(long id, Participant participant, Trial trial, int result) : base(id)
        {
            this.participant = participant;
            this.trial = trial;
            this.result = result;
        }

        public Result() : base(0) { }

        public Participant participant { get; set; }
        public Trial trial { get; set; }
        public int result { get; set; }
        
        [Column("participant_id")]

        public long ParticipantId { get; set; }
        
        [Column("trial_id")]
        public long TrialId { get; set; }
    }
}