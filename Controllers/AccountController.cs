using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        [HttpGet("{id}/balance")]
        public ActionResult<Account> GetAccount(long id)
        {
            var account = service.GetAccount(id);
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost("{id}/replenishment")]
        public ActionResult<Account> ReplenishmentBalance(long id, Transaction transaction)
        {
            var account = service.ReplenishmentBalance(id, transaction);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }

    }
}