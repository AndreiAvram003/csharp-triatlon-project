namespace Model.Domain
{
    public class RefereeUpdateDTO
    {
        public long Id { get; set; }
        public string Password { get; set; }
        public long trial_id{ get; set; }
        public string Name { get; set; }

        public RefereeUpdateDTO(long id, string password, string name, long trial_id)
        {
            Id = id;
            Password = password;
            Name = name;
            this.trial_id = trial_id;
        }
    }
}