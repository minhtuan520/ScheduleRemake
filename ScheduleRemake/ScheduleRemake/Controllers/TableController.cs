using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ScheduleRemake.Controllers
{
    [Route("api/Table")]
    [ApiController]
    public class TableController : ControllerBase
    {
        #region Declare
        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        #endregion
        #region Constructer
        public TableController(IUnitOfWork unitOfWork, ILogger<TableController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;          
        }
        #endregion        
        public string GetCookie(string key)
        {
            return Request.Cookies[key];
        }
        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }
        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }

        #region API
        [HttpGet]
        [Route("Get/{table}")]
        public IActionResult Get(string table)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(table))
                    return BadRequest("table cannot be null or empty");
                switch (table)
                {
                    case "LOP":
                        {
                            return Ok(_unitOfWork.Lop.GetClass());
                        }
                    case "MONHOC":
                        {
                            return Ok(_unitOfWork.MonHoc.GetSubject());
                        }
                    case "GIAOVIEN":
                        {
                            return Ok(_unitOfWork.GiaoVien.GetTeacher());
                        }
                    case "PHANCONG":
                        {
                            return Ok(_unitOfWork.PhanCong.GetRoster());
                        }
                    case "DIEUKIEN":
                        {
                            return Ok(_unitOfWork.DieuKien.GetCondition());
                        }
                    default: return BadRequest("ERROR");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost]
        [Route("Delete/{table}")]
        public bool DeleteTable(string table)
        {
            if (ModelState.IsValid)
            {
                bool result = false;
                if (string.IsNullOrWhiteSpace(table))
                    return false;
                switch (table)
                {
                    case "LOP":
                        {
                            result = _unitOfWork.Lop.DeleteClass();
                            break;
                        }
                    case "MONHOC":
                        {
                            result = _unitOfWork.MonHoc.DeleteSubject();
                            break;
                        }
                    case "GIAOVIEN":
                        {
                            result = _unitOfWork.GiaoVien.DeleteTeacher();
                            break;
                        }
                    case "PHANCONG":
                        {
                            result = _unitOfWork.PhanCong.DeleteRoster();
                            break;
                        }
                    case "DIEUKIEN":
                        {
                            result = _unitOfWork.DieuKien.DeleteRoster();
                            break;
                        }
                    default:
                        return false;
                }
                string user = Request.Cookies["User"];
                if (user == null) user = "";
                _unitOfWork.ThayDoi.AddChange("Delete", user, table);
                return result;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        [Route("Save/{table}")]
        public object Save(string table, [FromBody]object[] data)
        {
            string val = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Console.WriteLine(table + " = " + val);
            bool result = false;
            try
            {
                DeleteTable(table);
                switch (table)
                {
                    case "LOP":
                        {
                            List<Lop> Lops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Lop>>(val);
                            result = _unitOfWork.Lop.AddClass(Lops);
                            break;
                        }
                    case "MONHOC":
                        {
                            List<Monhoc> Monhocs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Monhoc>>(val);
                            result = _unitOfWork.MonHoc.AddSubjects(Monhocs);
                            break;
                        }
                    case "GIAOVIEN":
                        {
                            List<Giaovien> Giaoviens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Giaovien>>(val);
                            result = _unitOfWork.GiaoVien.AddTeachers(Giaoviens);
                            break;
                        }
                    case "PHANCONG":
                        {
                            List<Phancong> Phancongs = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Phancong>>(val);
                            result = _unitOfWork.PhanCong.AddRosters(Phancongs);
                            break;
                        }
                    case "DIEUKIEN":
                        {
                            List<Dieukien> Dieukiens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Dieukien>>(val);
                            result = _unitOfWork.DieuKien.AddConditions(Dieukiens);
                            break;
                        }
                    default:
                        return false;
                }
                string user = Request.Cookies["User"];
                if (user == null) user = "";
                result = _unitOfWork.ThayDoi.AddChange("Save", user, table);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result;
        }

    }
    #endregion
}