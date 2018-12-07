using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IScheduleRepository : IRepository<Tkb>
    {
        string GetClassSchedule(string Hieuluc, int Id, string L);
        string GetTeacherSchedule(string Hieuluc, int Id, string Gv);
        List<Tkb> GetSchedule(string Hieuluc, int Id);
        string GetAllSchedule();
        bool AddSchedules(List<Tkb> schedules);
        bool DeleteSchedules();
        bool SaveSchedules(List<Tkb> schedules);
        bool SaveTeacherSchedules(List<Tkb> schedules);
        bool SaveClassSchedules(List<Tkb> schedules);
    }
}
