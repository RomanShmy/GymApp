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
            var result = checkService.PostResult(subscriptionId);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/swimming")]
        public ActionResult<ResultHistory> AddResultSwimmingPool(long subscriptionId)
        {
            var result = checkService.PostResultSwimmingPool(subscriptionId);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/spa")]
        public ActionResult<ResultHistory> AddResultSpa(long subscriptionId)
        {
            var result = checkService.PostResultSpa(subscriptionId);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }
    }
}