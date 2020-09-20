using System.Collections.Generic;
using System.Net;
using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [EnableCors("MyPolicy")]
    [ApiController]
    [Route("api/[controller]")]
    public class CheckController : ControllerBase
    {
        private ICheckAccessService entryService;
        public CheckController(ICheckAccessService entryService)
        {
            this.entryService = entryService;
        }

        [HttpGet("{subscriptionId}/history")]
        public ActionResult<List<ResultHistory>> GetHistory(long subscriptionId)
        {
            var history = entryService.GetHistory(subscriptionId);
            if(history == null)
            {
                return NotFound();
            }
            return Ok(history);
        }

        [HttpPost("{subscriptionId}")]
        public ActionResult<ResultHistory> AddResult(long subscriptionId)
        {
            var result = entryService.CheckBalanceExpirationDateAndLogEntry(subscriptionId);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }

        [HttpPost("{subscriptionId}/{service}")]
        public ActionResult<ResultHistory> AddResultService(long subscriptionId, string service)
        {
            var result = entryService.CheckInclusiveServiceAndLogEntry(subscriptionId, service);
            return new CreatedResult(HttpContext.Request.Host + HttpContext.Request.Path, result);
        }
    }
}