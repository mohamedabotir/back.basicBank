namespace Server.BasicBank.Data.Entity
{
    public class Account
    {
        public Account(int id, string email, string name, double balance)
        {
            Id = id;
            Email = email;
            Name = name;
            Balance = balance;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }

        public double Balance { get; set; }
    }
}
