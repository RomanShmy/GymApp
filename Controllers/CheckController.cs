using System.Collections.Generic;
using System.Net;
using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private ICheckAccessService checkService;
        public CheckController(ICheckAccessService checkService)
        {
            this.checkService = checkService;
        }

        [HttpGet("{subscriptionId}/history")]
        public ActionResult<List<ResultHistory>> GetHistory(long subscriptionId)
        {
            var history = checkService.GetHistory(subscriptionId);
            if(history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost("{subscriptionId}")]
        public ActionResult<ResultHistory> AddResult(long subscriptionId)
        {
            var result = checkService.PostResult(subscriptionId);//checkbalance and log entry
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/swimming")]
        public ActionResult<ResultHistory> AddResultSwimmingPool(long subscriptionId)
        {
            string SWIMMING_POOL = "SWIMMING_POOL";
        
            var result = checkService.PostResultService(subscriptionId, SWIMMING_POOL);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/spa")]
        public ActionResult<ResultHistory> AddResultSpa(long subscriptionId)
        {
            const string SPA = "SPA";
            var result = checkService.PostResultService(subscriptionId, SPA);//entry service
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/gym")]
        public ActionResult<ResultHistory> AddResultGym(long subscriptionId)
        {
            const string GYM = "GYM";
            var result = checkService.PostResultService(subscriptionId, GYM);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }
    }
}