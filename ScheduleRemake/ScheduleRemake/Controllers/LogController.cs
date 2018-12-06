using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation;

namespace ScheduleRemake.Controllers
{
    [Route("api/Log")]
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    public class LogController : ControllerBase
    {
        #region Declare
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        #endregion
        #region Constructer
        public LogController(IUnitOfWork unitOfWork, ILogger<LogController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion
        #region API
        [HttpGet]
        [Route("Get/{table}")]
        public IActionResult Get(string table, string HieuLuc, int Id, string Lop = null, string GV = null)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(table))
                    return BadRequest("table cannot be null or empty");
                switch (table)
                {
                    case "LOG":
                        {
                            return Ok(_unitOfWork.Log.GetLog(HieuLuc, Id));
                        }
                    case "LOGGV":
                        {
                            if (string.IsNullOrWhiteSpace(GV))
                                return BadRequest("GV cannot be null or empty");
                            return Ok(_unitOfWork.Log.GetLogTeacher(HieuLuc, Id, GV));
                        }
                    case "LOGLOP":
                        {
                            if (string.IsNullOrWhiteSpace(Lop))
                                return BadRequest("Lop cannot be null or empty");
                            return Ok(_unitOfWork.Log.GetLogClass(HieuLuc, Id, Lop));
                        }
                    default: return BadRequest("ERROR");
                }
            }
            //
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion
    }
}