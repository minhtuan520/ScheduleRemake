using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface ILogRepository : IRepository<Log>
    {
        List<Log> GetLogClass(string Hieuluc, int Id, string L);
        List<Log> GetLogTeacher(string Hieuluc, int Id, string Gv);
        List<Log> GetLog(string Hieuluc, int Id);
        bool AddLogs(List<Log> logs);
        bool DeleteLogs();
    }
}
