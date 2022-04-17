namespace Server.BasicBank.Models
{
    public class TransferModel
    {
        public string senderEmail { get; set; }
        public string receiverEmail  { get; set; }
        public double money { get; set; }
    }
}
