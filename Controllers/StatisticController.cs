using System;
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
    public class StatisticController : ControllerBase
    {
        private IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet("type/now")]
        public ActionResult<List<CountByType>> GetStatisticByType()
        {
            return Ok(statisticService.GetByType());
        }

        [HttpGet("type/date")]
        public ActionResult<List<CountByType>> GetStatisticByTypeWithDate(DateTime from, DateTime to)
        {
            return Ok(statisticService.GetByTypeWithDate(from, to));
        }

        [HttpGet("visits")]
        public ActionResult<List<CountByType>> GetStatisticByVisitService()
        {
            return Ok(statisticService.GetQuantityTimeOfVisitAdditianalService());
        }

        [HttpGet("visits/date")]
        public ActionResult<List<CountByType>> GetStatisticByVisitService(DateTime from, DateTime to)
        {
            return Ok(statisticService.GetQuantityTimeOfVisitAdditianalService(from, to));
        }
    }
}