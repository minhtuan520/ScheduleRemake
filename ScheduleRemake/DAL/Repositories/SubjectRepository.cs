using DAL.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using System;

namespace DAL.Repositories
{
    public class SubjectRepository : Repository<Monhoc>, ISubjectRepository
    {
        public List<Monhoc> GetSubject()
        {
            var list = from monhoc in _appContext.Monhoc select monhoc;
            return list.ToList();
        }

        public bool DeleteSubject()
        {
            try
            {
                _appContext.RemoveRange(from mon in _appContext.Monhoc select mon);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public bool AddSubjects(List<Monhoc> subjects)
        {
            try
            {
                _appContext.Monhoc.AddRange(subjects);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public SubjectRepository(tkbremake4DbContext context) : base(context)
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
