using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Interfaces;
using Repository.Models.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TestTaskApi.Controllers
{
    [Route("api/[controller]")]
    public class IncidentController : Controller
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateIncident([FromBody] IncidentDTO incidentDto)
        {
            try
            {
                await _incidentService.CreateIncident(incidentDto);
                return Ok("Incident created successfully");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
