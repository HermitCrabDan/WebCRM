namespace WebCRM.WebApi.Controllers
{
    using WebCRM.Data;
    using WebCRM.Shared;
    using WebCRM.RoleSecurity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// CRM Api controller for Report Data
    /// </summary>
    /// <author>Daniel Lee Graf</author>

    [ApiController]
    [Route("api/[controller]")]
    public class ReportDataController: ControllerBase
    {
        private IReportRepository _repo;
        private IAppSecurityService _security;

        public ReportDataController(IReportRepository repo, IAppSecurityService security)
        {
            this._repo = repo;
            this._security = security;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this._repo.GetSummaryData());
        }
    }
}