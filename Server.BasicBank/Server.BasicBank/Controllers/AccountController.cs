using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.BasicBank.Data;
using Server.BasicBank.Data.Entity;
using Server.BasicBank.Models;

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
        public IActionResult getAllUsers()
        {
            var users = _ctx.Accounts.Select(e => e).AsEnumerable();

            return Ok(users);
        }

        [HttpPost]
        [Route(nameof(Transfer))]
        public async Task<IActionResult> Transfer([FromBody] TransferModel model)
        {
            var transaction = await _ctx.Database.BeginTransactionAsync();
            var sender = _ctx.Accounts.FirstOrDefault(e => e.Email == model.senderEmail);
            var receiver = _ctx.Accounts.FirstOrDefault(e => e.Email == model.receiverEmail);

            if (sender == null && receiver == null)
            {
                return BadRequest($"Sender and Receiver Account NotFound");

            }

            if (sender == null )
                return BadRequest($"Sender Account NotFound");
           if (receiver == null )
                return BadRequest($"Receiver Account NotFound");
           
            if (sender.Balance < model.money)
                return BadRequest("Money is not sufficient");
            sender.Balance -= model.money;
            receiver.Balance += model.money;
             _ctx.Accounts.Update(sender);
             _ctx.Accounts.Update(receiver);
            await _ctx.Transfers.AddAsync(new Transfers() {RecieverId = receiver.Id, SenderId = sender.Id, amount = model.money });
            await _ctx.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new {status="Successful Transfer"});
        }

        [HttpGet]
        [Route(nameof(GetTransactions))]
        public IActionResult GetTransactions()
        {
            var transactions = _ctx.Transfers.Select(e => e).Include(e=>e.Reciever).Include(e=>e.Sender).AsEnumerable();
            IEnumerable<TransferModel> model = new List<TransferModel>();
            foreach (var trans in transactions)
            {
                var sender = _ctx.Accounts.SingleOrDefault(e => e.Id == trans.SenderId);
                var receiver = _ctx.Accounts.SingleOrDefault(e => e.Id == trans.RecieverId);
                model.Append(new TransferModel()
                {
                    senderEmail = sender.Name,
                    receiverEmail = receiver.Name,
                    money = trans.amount
                });
            }
            return Ok(transactions);
        }
    }
}
