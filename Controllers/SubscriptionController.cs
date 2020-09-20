using System.Collections.Generic;
using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionController : ControllerBase
    {
        private ISubscriptionService subscriptionService; 

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            this.subscriptionService = subscriptionService;
        }  

        public ActionResult<List<Subscription>> GetSubscriptions(){
            return Ok(subscriptionService.GetSubscriptions());
        }

        [HttpGet("{id}")]
        public ActionResult<Subscription> GetSubscription(long id)
        {
            var subscription = subscriptionService.GetSubscription(id);
            if(subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }

        [HttpPost]
        public ActionResult<Subscription> AddSubscription([FromBody]Subscription subscription)
        {
            var sub = subscriptionService.AddSubscription(subscription);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, sub);
        }

    }
}