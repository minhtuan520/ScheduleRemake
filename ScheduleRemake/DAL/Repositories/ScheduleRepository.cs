using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ScheduleRepository : Repository<Tkb>, IScheduleRepository
    {
        #region Constructer
        public ScheduleRepository(tkbremake4DbContext context) : base(context)
        {
        }
        private tkbremake4DbContext _appContext
        {
            get
            {
                return (tkbremake4DbContext)_context;
            }
        }
        #endregion
        #region Function
        public string GetAllSchedule()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                    from r in _appContext.Tkb
                    group r by new { r.Hieuluc, r.Id }
                    into t
                    select new { Hieuluc = t.Key.Hieuluc, Id = t.Key.Id });
        }

        public string GetClassSchedule(string Hieuluc, int Id, string L)
        {
            if (Hieuluc == null) Hieuluc = "";
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                from r in _appContext.Tkb
                where r.Hieuluc == Hieuluc && r.Id == Id && r.L == L
                select r);

        }
        public string GetSchedule(string Hieuluc, int Id)
        {
            if (Hieuluc == null) Hieuluc = "";
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                    from r in _appContext.Tkb
                    where r.Hieuluc == Hieuluc && r.Id == Id
                    select r);
        }

        public string GetTeacherSchedule(string Hieuluc, int Id, string Gv)
        {
            if (Hieuluc == null) Hieuluc = "";
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                    from r in _appContext.Tkb
                    where r.Hieuluc == Hieuluc && r.Id == Id && r.Gv == Gv
                    select r);
        }
        public bool AddSchedules(List<Tkb> schedules)
        {
            _appContext.Tkb.AddRange(schedules);
            return true;
        }

        public bool DeleteSchedules()
        {
            try
            {
                _appContext.Tkb.RemoveRange(from tkb in _appContext.Tkb where tkb.Hieuluc == "" select tkb);
                _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
        #endregion
    }
}
