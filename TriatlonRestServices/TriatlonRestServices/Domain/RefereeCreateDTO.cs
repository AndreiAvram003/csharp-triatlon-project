namespace Model.Domain
{
    public class RefereeCreateDTO
    {
        public string Password { get; set; }
        public long trial_id { get; set; }
        public string Name { get; set; }

        public RefereeCreateDTO(string password, string name, long trial_id)
        {
            Password = password;
            Name = name;
            this.trial_id = trial_id;
        }
    }
}