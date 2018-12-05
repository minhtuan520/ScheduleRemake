// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {      
        #region declare
        readonly tkbremake4DbContext _context;

        private IClassRepository _class;
        private ISubjectRepository _subject;
        private ITeacherRepository _teacher;
        private IRosterRepository _roster;
        private IConditionRepository _condition;
        private IAccountRepository _account;
        private IChangeRepository _change;
        private ILogRepository _log;
        private IScheduleRepository _schedule;
        #endregion

        public UnitOfWork(tkbremake4DbContext context)
        {
            _context = context;
        }
        #region extract
        public IChangeRepository ThayDoi
        {
            get
            {
                if (_change == null)
                    _change = new ChangeRepository(_context);

                return _change;
            }
        }
        public IClassRepository Lop
        {
            get
            {
                if (_class == null)
                    _class = new ClassRepository(_context);

                return _class;
            }
        }
        public ISubjectRepository MonHoc
        {
            get
            {
                if (_subject == null)
                    _subject = new SubjectRepository(_context);

                return _subject;
            }
        }
        public IRosterRepository PhanCong
        {
            get
            {
                if (_roster == null)
                    _roster = new RosterRepository(_context);

                return _roster;
            }
        }
        public ITeacherRepository GiaoVien
        {
            get
            {
                if (_teacher == null)
                    _teacher = new TeacherRepository(_context);

                return _teacher;
            }
        }
        public IConditionRepository DieuKien
        {
            get
            {
                if (_condition == null)
                    _condition = new ConditionRepository(_context);

                return _condition;
            }
        }
        public IAccountRepository TaiKhoan
        {
            get
            {
                if (_account == null)
                    _account = new AccountRepository(_context);

                return _account;
            }
        }
        public ILogRepository Log
        {
            get
            {
                if (_log == null)
                    _log = new LogRepository(_context);

                return _log;
            }
        }
        public IScheduleRepository TKB
        {
            get
            {
                if (_schedule == null)
                    _schedule = new ScheduleRepository(_context);

                return _schedule;
            }
        }
        #endregion

        //public int SaveChanges()
        //{
        //    return _context.SaveChanges();
        //}
    }
}
