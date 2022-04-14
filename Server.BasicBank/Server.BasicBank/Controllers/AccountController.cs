using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.BasicBank.Data;
using Server.BasicBank.Data.Entity;
 
namespace Server.BasicBank.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
       public ApplicationDbContext _ctx;

        public AccountController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        [Route(nameof(getAllUsers))]
        public async Task<IActionResult> getAllUsers()
        {
            var users = _ctx.Accounts.Select(e => e);

            return Ok(users);
        }

        [HttpPost]
        [Route(nameof(Transfer))]
        public async Task<IActionResult> Transfer(string senderEmail,string receiverEmail,double money)
        {
            var transaction = await _ctx.Database.BeginTransactionAsync();
            var sender = _ctx.Accounts.FirstOrDefault(e => e.Email == senderEmail);
            var receiver = _ctx.Accounts.FirstOrDefault(e => e.Email == receiverEmail);

            if (sender == null || receiver == null)
                return BadRequest("Account NotFound");
            if (sender.Balance < money)
                return BadRequest("Money is not sufficient");
            sender.Balance -= money;
            receiver.Balance += money;
             _ctx.Accounts.Update(sender);
             _ctx.Accounts.Update(receiver);
            await _ctx.Transfers.AddAsync(new Transfers() {RecieverId = receiver.Id, SenderId = sender.Id, amount = money});
            await _ctx.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new {status="Successfully"});
        }
    }
}
