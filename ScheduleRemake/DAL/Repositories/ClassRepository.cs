using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ClassRepository : Repository<Lop>, IClassRepository
    {      
        public List<Lop> GetClass()
        {
            var list = from lop in _appContext.Lop select lop;
            return list.ToList();
        }

        public ClassRepository(tkbremake4DbContext context) : base(context)
        { }

        private tkbremake4DbContext _appContext
        {
            get
            {
                return (tkbremake4DbContext)_context;
            }
        }

       
    

        public bool DeleteClass()
        {
            try
            {
                _appContext.RemoveRange(from lop in _appContext.Lop select lop);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public bool AddClass(List<Lop> classes)
        {
            try
            {
                _appContext.Lop.AddRange(classes);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }
    }
}
