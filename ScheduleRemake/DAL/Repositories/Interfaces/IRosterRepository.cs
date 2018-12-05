using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IRosterRepository : IRepository<Phancong>
    {
        List<Phancong> GetRoster();
        bool AddRosters(List<Phancong> rosters);
        bool DeleteRoster();
    }
}
