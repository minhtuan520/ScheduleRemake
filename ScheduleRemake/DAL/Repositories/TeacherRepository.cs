using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class TeacherRepository : Repository<Giaovien>, ITeacherRepository
    {

        public List<Giaovien> GetTeacher()
        {
            var list = from giaovien in _appContext.Giaovien select giaovien;
            return list.ToList();
        }

        public bool DeleteTeacher()
        {
            try
            {
                _appContext.RemoveRange(from gv in _appContext.Giaovien select gv);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public bool AddTeachers(List<Giaovien> teachers)
        {
            try
            {
                _appContext.Giaovien.AddRange(teachers);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public TeacherRepository(tkbremake4DbContext context) : base(context)
        {
        }
        private tkbremake4DbContext _appContext
        {
            get
            {
                return (tkbremake4DbContext)_context;
            }
        }
    }
}
