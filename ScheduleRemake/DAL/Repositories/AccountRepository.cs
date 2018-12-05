using DAL.Models;
using DAL.Repositories.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using System;

namespace DAL.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public bool[] Login(string Username, string Password)
        {
            var Account = (from acc in _appContext.Account where acc.User == Username select acc).ToList();
            //khong co user            
            if (Account.Count == 0) return new bool[2] { false, false };
            //dang nhap thanh cong
            if (Account[0].Pass == Password)
            {
                //CookieOptions opt = new CookieOptions();
                //opt.Expires = DateTime.Now.AddMinutes(15);
                //Response.Cookies.Append("User", User, opt);
                return new bool[2] { true, true };
            }
            //sai pass
            else return new bool[2] { true, false };
        }

        public AccountRepository(tkbremake4DbContext context) : base(context)
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
