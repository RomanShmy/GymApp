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

        [HttpGet]
        public ActionResult<Account> GetAccount()
        {
            var account = service.GetAccount();
            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }
    }
}