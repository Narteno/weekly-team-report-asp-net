using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Repositories;

namespace CM.WeeklyTeamReport.WebApp.Controllers
{
    [Route("api/companies/{companyId}/members/{memberId}/reports")]
    public class WeeklyReportController : ControllerBase
    {
        private readonly IRepository<WeeklyReport> _repository;
        public WeeklyReportController(IRepository<WeeklyReport> repository)
        {
            _repository = repository;
        }
        [HttpGet("{reportId}")]
        public IActionResult Get(int reportId)
        {
            var result = _repository.Read(reportId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetAllByMember(int memberId)
        {
            var result = _repository.ReadAllByParentId(memberId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromQuery] WeeklyReport weeklyReport)
        {
            var result = _repository.Create(weeklyReport);
            if (result == null) return BadRequest();
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put([FromQuery] WeeklyReport weeklyReport)
        {
            var result = _repository.Read(weeklyReport.ReportId);
            if (result == null) return NotFound();
            _repository.Update(weeklyReport);
            return Ok();
        }

        [HttpDelete("{reportId}")]
        public IActionResult Delete(int reportId)
        {
            var result = _repository.Read(reportId);
            if (result == null) return NotFound();
            _repository.Delete(reportId);
            return Ok();
        }
    }
}
