using DAL.Models;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repositories
{
    public class ChangeRepository : Repository<Change>, IChangeRepository
    {    
        public ChangeRepository(tkbremake4DbContext context) : base(context)
        {
        }
        private tkbremake4DbContext _appContext
        {
            get
            {
                return (tkbremake4DbContext)_context;
            }
        }

        public bool AddChange(string action, string user, string table)
        {
            try
            {
                _appContext.Change.Add(new Change() { User = user, Action = action, Target = table, Time = System.DateTime.Now.ToString() });
                _appContext.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
           
        }
    }
}
