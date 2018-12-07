using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class LogRepository : Repository<Log>, ILogRepository
    {
        #region Constructer
        public LogRepository(tkbremake4DbContext context) : base(context)
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

        public List<Log> GetLog(string Hieuluc, int Id)
        {
            if (Hieuluc == null)
            {
                Hieuluc = "";
            }
            List<Log> result = (from log in _appContext.Log
             where log.Hieuluc == Hieuluc && log.Id == Id
             select log).ToList();
            return result;        
        }

        public List<Log> GetLogClass(string Hieuluc, int Id, string L)
        {
            if (Hieuluc == null)
            {
                Hieuluc = "";
            }
            List<Log> result = (from log in _appContext.Log
                                where log.Hieuluc == Hieuluc && log.Id == Id && log.L == L
                                select log).ToList();
            return result;
        }

        public List<Log> GetLogTeacher(string Hieuluc, int Id, string Gv)
        {
            if (Hieuluc == null)
            {
                Hieuluc = "";
            }
            List<Log> result = (from log in _appContext.Log
                                where log.Hieuluc == Hieuluc && log.Id == Id && log.Gv == Gv
                                select log).ToList();
            return result;
        }
        public bool AddLogs(List<Log> logs)
        {
            try
            {
                _appContext.Log.AddRange(logs);
                _appContext.SaveChanges();
            } catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public bool DeleteLogs()
        {
            try
            {
                _appContext.Log.RemoveRange(from log in _appContext.Log where log.Hieuluc == "" select log);
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
