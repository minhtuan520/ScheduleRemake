using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace DAL.Repositories
{
    public class ConditionRepository : Repository<Dieukien>, IConditionRepository
    {

        public List<Dieukien> GetCondition()
        {
            var list = from dieukien in _appContext.Dieukien select dieukien;
            return list.ToList();
        }

        public bool DeleteRoster()
        {
            try
            {
                _appContext.RemoveRange(from dk in _appContext.Dieukien select dk);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public bool AddConditions(List<Dieukien> conditions)
        {
            try
            {
                _appContext.Dieukien.AddRange(conditions);
                _appContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
            return true;
        }

        public ConditionRepository(tkbremake4DbContext context) : base(context)
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
