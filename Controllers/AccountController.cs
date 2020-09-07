using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService accountService;//account accountService
        

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("{id}/balance")]
        public ActionResult<Account> GetAccount(long id)
        {
            var account = accountService.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost("{id}/replenishment")]
        public ActionResult<Account> ReplenishmentBalance(long id, Transaction transaction)
        {
            var account = accountService.ReplenishmentBalance(id, transaction);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

        [HttpPost("{id}/withdrawal")]
        public ActionResult<Account> WithdrawalBalance(long id, Transaction transaction)
        {
            var account = accountService.WithdrawalBalance(id, transaction);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

    }
}