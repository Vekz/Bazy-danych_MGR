namespace VegeShama.Common.DatabaseModels.Relational
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte Type { get; set; }

        public Guid CustomerId { get; set; }
    }
}
