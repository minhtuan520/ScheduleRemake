using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IChangeRepository : IRepository<Change>
    {
        bool AddChange(string action,string user, string table);
    }
}
