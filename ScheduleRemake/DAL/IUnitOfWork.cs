// ====================================================
// More Templates: https://www.ebenmonney.com/templates
// Email: support@ebenmonney.com
// ====================================================

using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {       
        IClassRepository Lop { get; }
        ISubjectRepository MonHoc { get; }
        ITeacherRepository GiaoVien { get; }
        IRosterRepository PhanCong { get; }
        IConditionRepository DieuKien { get; }
        IAccountRepository TaiKhoan { get; }
        IChangeRepository ThayDoi { get; }
        ILogRepository Log { get; }
        IScheduleRepository TKB { get; }
        //int SaveChanges();
    }
}
