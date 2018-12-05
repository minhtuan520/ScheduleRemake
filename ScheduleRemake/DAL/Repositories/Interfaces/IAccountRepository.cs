using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        bool[] Login(string Username, string Password);
    }
}
