using System;
using System.Collections.Generic;
using GymApp.Models;
using GymApp.Services.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : ControllerBase
    {
        private IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }

        [HttpGet("now")]
        public ActionResult<List<CountByType>> GetStatisticByType()
        {
            return Ok(statisticService.GetByType());
        }

        [HttpGet]
        public ActionResult<List<CountByType>> GetStatisticByTypeWithDate(DateTime from, DateTime to)
        {
            return Ok(statisticService.GetByTypeWithDate(from, to));
        }
    }
}