using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenIddict.Validation;

namespace ScheduleRemake.Controllers
{
    [Route("api/Schedule")]
    [ApiController]
    [Authorize(AuthenticationSchemes = OpenIddictValidationDefaults.AuthenticationScheme)]
    public class ScheduleController : ControllerBase
    {
        #region Declare
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        #endregion
        #region Constructer
        public ScheduleController(IUnitOfWork unitOfWork, ILogger<ScheduleController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion
        #region API
        [HttpGet]
        [Route("Get/{table}")]
        public IActionResult Get(string table, string HieuLuc = null, int Id = -1, string Lop = null, string GV = null)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(table))
                    return BadRequest("table cannot be null or empty");
                switch (table)
                {
                    case "Schedule":
                        {
                            if (string.IsNullOrWhiteSpace(HieuLuc) || Id == -1)
                                return BadRequest("variable cannot be null or empty");
                            return Ok(_unitOfWork.TKB.GetSchedule(HieuLuc, Id));
                        }
                    case "TeacherSchedule":
                        {
                            if (string.IsNullOrWhiteSpace(HieuLuc) || Id == -1 || string.IsNullOrWhiteSpace(GV))
                                return BadRequest("variable cannot be null or empty");

                            return Ok(_unitOfWork.TKB.GetTeacherSchedule(HieuLuc, Id, GV));
                        }
                    case "ClassSchedule":
                        {
                            if (string.IsNullOrWhiteSpace(HieuLuc) || Id == -1 || string.IsNullOrWhiteSpace(Lop))
                                return BadRequest("variable cannot be null or empty");
                            return Ok(_unitOfWork.TKB.GetClassSchedule(HieuLuc, Id, Lop));
                        }
                    case "All":
                        {
                            return Ok(_unitOfWork.TKB.GetAll());
                        }
                    default: return BadRequest("ERROR");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion
    }
}