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
        public List<Tkb> GetSchedule(string Hieuluc, int Id)
        {
            if (Hieuluc == null) Hieuluc = "";
            return (
                    from r in _appContext.Tkb
                    where r.Hieuluc == Hieuluc && r.Id == Id
                    select r).ToList();
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
            try
            {
                _appContext.Tkb.AddRange(schedules);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
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

        public bool SaveSchedules(List<Tkb> schedules)
        {
            List<Tkb> NewSchedules = new List<Tkb>();
            for (int i = 0; i < schedules.Count; i++)
            {
                var FoundSchedule = (from s in _appContext.Tkb where schedules[i].Hieuluc == s.Hieuluc && schedules[i].Id == s.Id select s).ToList();
                if (FoundSchedule.Count == 0)//ko trung
                {
                    NewSchedules.Add(schedules[i]);
                }
                else//trung
                {
                    _appContext.Tkb.Remove(FoundSchedule[0]);//xoa cai da ton tai
                    NewSchedules.Add(schedules[i]);// add lai cai moi
                }
            }
            try
            {
                _appContext.Tkb.AddRange(NewSchedules);
                _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }
        public bool SaveTeacherSchedules(List<Tkb> schedules)
        {
            List<Tkb> NewSchedules = new List<Tkb>();//dung de them nhung cai moi vao, cuoi cung bo vao db
            for (int i = 0; i < schedules.Count; i++)
            {
                var FoundSchedule = (from s in _appContext.Tkb where schedules[i].Hieuluc == s.Hieuluc && schedules[i].Id == s.Id && schedules[i].Gv == s.Gv select s).ToList();
                if (FoundSchedule.Count == 0)//ko trung
                {
                    NewSchedules.Add(schedules[i]);
                }
                else//trung
                {
                    _appContext.Tkb.Remove(FoundSchedule[0]);//xoa cai da ton tai
                    NewSchedules.Add(schedules[i]);// add lai cai moi
                }
            }
            try
            {
                _appContext.Tkb.AddRange(NewSchedules);
                _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public bool SaveClassSchedules(List<Tkb> schedules)
        {
            List<Tkb> NewSchedules = new List<Tkb>();//dung de them nhung cai moi vao, cuoi cung bo vao db
            for (int i = 0; i < schedules.Count; i++)
            {
                var FoundSchedule = (from s in _appContext.Tkb where schedules[i].Hieuluc == s.Hieuluc && schedules[i].Id == s.Id && schedules[i].L == s.L select s).ToList();
                if (FoundSchedule.Count == 0)//ko trung
                {
                    NewSchedules.Add(schedules[i]);
                }
                else//trung
                {
                    _appContext.Tkb.Remove(FoundSchedule[0]);//xoa cai da ton tai
                    NewSchedules.Add(schedules[i]);// add lai cai moi
                }
            }
            try
            {
                _appContext.Tkb.AddRange(NewSchedules);
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
