namespace Server.BasicBank.Data.Entity
{
    public class Transfers
    {
        public int Id { get; set; }
        public Account Sender { get; set; }
        public int SenderId { get; set; }
        public Account Reciever { get; set; }
        public int RecieverId { get; set; }

        public double amount { get; set; }
    }
}
