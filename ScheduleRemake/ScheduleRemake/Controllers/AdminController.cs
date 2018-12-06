using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ScheduleRemake.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        #region Declare
        X[] kq;
        int d_kq, limit = 1;
        int tong_tiet;
        Monhoc[] dsMH;
        sLOP[] dsL;
        sGIAOVIEN[] dsGV;
        Phancong[] dsPC;
        bool[] ktPC;
        static bool ready = true;

        private IUnitOfWork _unitOfWork;
        readonly ILogger _logger;
        #endregion
        #region Constructer
        public AdminController(IUnitOfWork unitOfWork, ILogger<AdminController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        #endregion
        #region API
        [HttpPost]
        [Route("Solve")]
        public bool Solve_ver2(int tongtiet)
        {
            //string user = Request.Cookies["User"];
            //if (user == null) Response.Redirect("/Login");
            //else if (user != "admin") Response.Redirect("/Error/200");

            if (ready)
            {
                ready = false;
                tong_tiet = tongtiet; d_kq = 0;
                Solve();
                return true;
            }

            return false;
        }
        #endregion
        #region Function
        void Solve()
        {
            //get MONHOC
            dsMH = _unitOfWork.MonHoc.GetSubject().ToArray();
            //get LOP
            var tmpL = _unitOfWork.Lop.GetClass().ToArray();
            //moi lop co tiet ban va so tiet chua day
            dsL = new sLOP[tmpL.Count()];
            for (int i = 0; i < tmpL.Count(); i++)
            {
                dsL[i] = new sLOP(tong_tiet, dsMH.Count());
                dsL[i].L = tmpL[i].L;

                for (int j = 0; j < dsMH.Count(); j++)
                {
                    dsL[i].dsMH[j].Mh = dsMH[j].Mh;
                    dsL[i].dsMH[j].Lap = dsMH[j].Lap;
                }
            }
            //get GIAOVIEN
            var tmpGV = _unitOfWork.GiaoVien.GetTeacher().ToArray();
            //moi giaovien co tiet ban
            dsGV = new sGIAOVIEN[tmpGV.Count()];
            for (int i = 0; i < tmpGV.Count(); i++)
            {
                dsGV[i] = new sGIAOVIEN(tong_tiet);
                dsGV[i].GV = tmpGV[i].Gv;
            }

            //get PHANCONG
            dsPC = _unitOfWork.PhanCong.GetRoster().ToArray();

            //khoi tao 1 tkb
            kq = new X[dsPC.Count()];
            for (int i = 0; i < dsPC.Count(); i++)
                kq[i] = new X();

            //get DIEUKIEN
            var dsDK = _unitOfWork.DieuKien.GetCondition().ToArray();
            //kiem tra phancong da hoan thanh chua
            ktPC = new bool[dsPC.Count()];
            for (int i = 0; i < dsDK.Count(); i++)
            {
                //xep cac tiet trong DIEUKIEN truoc
                Phancong tmpPC = new Phancong { Gv = dsDK[i].Gv, Mh = dsDK[i].Mh, L = dsDK[i].L };
                int idPC = findPHANCONG(tmpPC);
                if (idPC > 0)
                {
                    ktPC[idPC] = true;
                    int tiet = toTiet(dsDK[i]);
                    //dieu kien > gioi han tong tiet
                    if (tiet > tong_tiet) continue;
                    int idGV = findGIAOVIEN(dsDK[i].Gv);
                    int idL = findLOP(dsDK[i].L);
                    int idMH = findMONHOC(dsDK[i].Mh);

                    dsGV[idGV].busy[tiet] = true;
                    dsL[idL].busy[tiet] = true;
                    dsL[idL].dsMH[idMH].Lap--;

                    kq[idPC].dsTIET.Add(tiet);
                }
            }
            _unitOfWork.TKB.DeleteSchedules();
            _unitOfWork.Log.DeleteLogs();               
            phantiet(0);
        }
        int findPHANCONG(Phancong PC)
        {
            for (int i = 0; i < dsPC.Count(); i++)
                if (PC.Gv == dsPC[i].Gv && PC.Mh == dsPC[i].Mh && PC.L == dsPC[i].L)
                    return i;
            return 0;
        }

        int findGIAOVIEN(string GV)
        {
            for (int i = 0; i < dsGV.Count(); i++)
                if (GV == dsGV[i].GV)
                    return i;
            return 0;
        }

        int findLOP(string L)
        {
            for (int i = 0; i < dsL.Count(); i++)
                if (L == dsL[i].L)
                    return i;
            return 0;
        }

        int findMONHOC(string MH)
        {
            for (int i = 0; i < dsMH.Count(); i++)
                if (MH == dsMH[i].Mh)
                    return i;
            return 0;
        }

        int toTiet(Dieukien dk)
        {
            return 5 * dk.Thu + dk.Tiet - 10;
        }

        Dieukien toDieukien(int tiet, Phancong pc)
        {
            var tmpDK = new Dieukien();
            tmpDK.Thu = (short)(tiet / 5 + 2);
            tmpDK.Tiet = (short)(tiet % 5);
            if (tmpDK.Tiet == 0)
            {
                tmpDK.Tiet = 5;
                tmpDK.Thu--;
            }

            tmpDK.Gv = pc.Gv;
            tmpDK.L = pc.L;
            tmpDK.Mh = pc.Mh;
            return tmpDK;
        }

        void phantiet(int pc)
        {
            if (d_kq >= limit) return;
            if (pc == dsPC.Count())
            {
                inkq();
                return;
            }
            if (ktPC[pc])
            {
                phantiet(pc + 1);
                return;
            }
            bool kt = true;
            for (int tiet = 1; tiet <= tong_tiet; tiet++)
            {
                int idGV = findGIAOVIEN(dsPC[pc].Gv);
                int idMH = findMONHOC(dsPC[pc].Mh);
                int idL = findLOP(dsPC[pc].L);

                if (!dsGV[idGV].busy[tiet] && !dsL[idL].busy[tiet] && dsL[idL].dsMH[idMH].Lap > 0)
                {
                    kt = false;
                    dsGV[idGV].busy[tiet] = true;
                    dsL[idL].busy[tiet] = true;
                    dsL[idL].dsMH[idMH].Lap--;

                    kq[pc].dsTIET.Add(tiet);
                    phantiet(pc);
                    kq[pc].dsTIET.Remove(tiet);

                    dsGV[idGV].busy[tiet] = false;
                    dsL[idL].busy[tiet] = false;
                    dsL[idL].dsMH[idMH].Lap++;
                }
            }

            if (kt)
                phantiet(pc + 1);
        }

        void inkq()
        {
            d_kq++;
            List<Tkb> Schedules = new List<Tkb>();
            List<Log> Logs = new List<Log>();
            //ghi TKB
            for (int i = 0; i < dsPC.Count(); i++)
            {
                foreach (int o in kq[i].dsTIET)
                {
                    Dieukien tmpDK = toDieukien(o, dsPC[i]);
                    Tkb Schedule = new Tkb
                    {
                        Id = d_kq,
                        Hieuluc = "",
                        Gv = tmpDK.Gv,
                        Mh = tmpDK.Mh,
                        L = tmpDK.L,
                        Tiet = tmpDK.Tiet,
                        Thu = tmpDK.Thu
                    };
                    Schedules.Add(Schedule);
                }
            }               
            //ghi LOG
            for (int i = 0; i < dsL.Count(); i++)
            {
                for (int j = 0; j < dsMH.Count(); j++)
                {
                    if (dsL[i].dsMH[j].Lap > 0)
                    {
                        for (int k = 0; k < dsPC.Count(); k++)
                        {
                            if (dsPC[k].L == dsL[i].L && dsPC[k].Mh == dsL[i].dsMH[j].Mh)
                            {
                                Log log = new Log
                                {
                                    Id = d_kq,
                                    Hieuluc = "",
                                    Gv = dsPC[k].Gv,
                                    Mh = dsPC[k].Mh,
                                    L = dsPC[k].L,
                                    Tiet = (int)dsL[i].dsMH[j].Lap
                                };
                                Logs.Add(log);
                            }                          
                        }
                    }
                }
            }
            _unitOfWork.TKB.AddSchedules(Schedules);
            _unitOfWork.Log.AddLogs(Logs);                       
            ready = true;
        }
        #endregion

        #region Class
        class X
        {
            public List<int> dsTIET = new List<int>();
        }
        class sLOP
        {
            public string L;
            public bool[] busy;
            public Monhoc[] dsMH;
            public sLOP(int tiet, int d_mh)
            {
                busy = new bool[tiet + 1];
                dsMH = new Monhoc[d_mh];

                for (int i = 0; i < d_mh; i++)
                    dsMH[i] = new Monhoc();
            }
        }

        class sGIAOVIEN
        {
            public string GV;
            public bool[] busy;
            public sGIAOVIEN(int tiet)
            {
                busy = new bool[tiet + 1];
            }
        }
        #endregion
    }
}