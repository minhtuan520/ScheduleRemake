using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IConditionRepository : IRepository<Dieukien>
    {
        List<Dieukien> GetCondition();
        bool AddConditions(List<Dieukien> conditions);
        bool DeleteRoster();
    }
}
