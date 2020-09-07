using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private ISubscriptionService subscriptionService; 

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }  

        [HttpPost]
        public ActionResult<Subscription> AddSubscription([FromBody]Subscription subscription)
        {
            var sub = subscriptionService.AddSubscription(subscription);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, sub);
        }

    }
}