using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface ITeacherRepository : IRepository<Giaovien>
    {
        List<Giaovien> GetTeacher();
        bool AddTeachers(List<Giaovien> teachers);
        bool DeleteTeacher();
    }
}
