using Kain_class.ExceDR;
using Kain_class.Json;
using Kain_class.Messageinfor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kain_class.HtmlSubstring;
using System.Configuration;
using Kain_class.Pass;
using Kain_class.SqlPager;
using Dinner.Models;
using Dinner.Models.Filter;
using System.Text;
using System.Collections;
using NPOI.HSSF.UserModel;
using Kain_class.ExcelHead;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using Kain_class.ExceDC;

namespace Dinner.Controllers
{
    public class HomeController : MainsController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 导入客户
        /// </summary>
        /// <returns></returns>
        public ActionResult DataInsert()
        {
            return View();
        }
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ManagerMeetting()
        {
            return View();
        }
        /// <summary>
        /// 新会议
        /// </summary>
        /// <returns></returns>
        [LVLFilter(Orderid = 1)]
        public ActionResult NewMeetting()
        {
            return View();
        }
        /// <summary>
        /// 负责人管理界面
        /// </summary>
        /// <returns></returns>
         [LVLFilter(Orderid = 1)]
        public ActionResult Fzr()
        {
          
            string sql = @"select UserName,Bank,Start_Date,End_Date,a.id,Meeting,b.MeetingName,a.GamePwd from tb_login a left join tb_Meeting b on a.Meeting=b.id where Lvl=1 and a.isdel=0 order by a.id desc";

            MessasgeData mgdata = Datafun.MgfunctionData(sql);

            List<tb_login> listorder = new List<tb_login>();
            if (mgdata.Mgdatacount > 0)
            {
                foreach (DataRow dr in mgdata.Mgdata.Rows)
                {
                    tb_login cbai = new tb_login
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        UserName = dr["UserName"].ToString(),
                        Bank =dr["Bank"].ToString(),
                        Start_Date = Convert.ToDateTime(dr["Start_Date"].ToString()),
                        End_Date = Convert.ToDateTime(dr["End_Date"].ToString()),
                        Meeting = Convert.ToInt32(dr["Meeting"].ToString()),
                        added1 = dr["MeetingName"].ToString(),
                        GamePwd = dr["GamePwd"].ToString()

                    };
                    listorder.Add(cbai);
                };

            }
       
            return View(listorder);
        }
        /// <summary>
        /// 增加负责人
        /// </summary>
        /// <returns></returns>
        [LVLFilter(Orderid = 1)]
        public ActionResult AddFzr()
        {
            return View();
        }
        /// <summary>
        /// 录入数据（现金 红包）
        /// </summary>
        /// <returns></returns>
        public ActionResult Cash()
        {
            return View();
        }
        /// <summary>
        /// 设置桌子
        /// </summary>
        /// <returns></returns>
        public ActionResult SetDesk()
        {
            return View();
        }
        /// <summary>
        /// 设置桌子名称
        /// </summary>
        /// <returns></returns>
        public ActionResult SetDeskName()
        {
            MessasgeData mgdata = Datafun.MgfunctionData("select id, Zh from tb_znum where Meeting=@Meeting",new SqlParameter("@Meeting",LgUser.Meetting));
            ViewBag.table = mgdata.Mgdata;
            return View();
        }
        /// <summary>
        /// 统计数据
        /// </summary>
        /// <returns></returns>
       
        public ActionResult Tjgift()
        {
            return View();
        }
        /// <summary>
        /// 设置游戏开始时间
        /// </summary>
        /// <returns></returns>
        public ActionResult SetGametime()
        {
            return View();
        }
        /// <summary>
        /// 礼物详情单
        /// </summary>
        /// <returns></returns>
        public ActionResult GiftInfor(string id)
        {
            MessasgeData mgdata = Datafun.MgfunctionData("select a.id,UserName,Tel,Qdh,Hongbao,Gift,b.MeetingName from tb_gift A left join tb_Meeting b on a.Meeting=b.id where a.isdel=0 and Meeting=@id", new SqlParameter("@id", id));
            List<tb_gift> listorder = new List<tb_gift>();
            foreach (DataRow dr in mgdata.Mgdata.Rows)
            {
                tb_gift cbai = new tb_gift
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    UserName = dr["UserName"].ToString(),
                    Tel = dr["Tel"].ToString(),
                    Qdh = dr["Qdh"].ToString(),
                    Hongbao =dr["Hongbao"].ToString(),
                    Gift = dr["Gift"].ToString(),
                    Added1 = dr["MeetingName"].ToString()
                };
                listorder.Add(cbai);
            };
            return View(listorder);
        }
        /// <summary>
        /// 设置页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Set()
        {
            ViewBag.lvl = LgUser.UserLvl;
            return View();
        }
       
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Manager()
        {
            ViewBag.lvl = LgUser.UserLvl;
            return View();
           
        }
        /// <summary>
        /// Admin Index
        /// </summary>
        /// <returns></returns>
        [LVLFilter(Orderid = 1)]
        public ActionResult AdminIndex(string id,string ids)
        {
            int pageIndex = Convert.ToInt32(id ?? "0");

            if (id == "0")
            {
                pageIndex = 1;
            }
            string sql = @"select UserName,Bank,convert(nvarchar(11),Start_Date,120) as Start_Date ,convert(nvarchar(11),End_Date,120) as End_Date,a.id,Meeting,b.MeetingName from tb_login a left join tb_Meeting b on a.Meeting=b.id where Lvl=1 and a.isdel=1 ";

            MessasgeData mgdata = SqlPage.SqlPageAction(Datafun, sql, "id desc ", pageIndex, 10);

            List<tb_login> listorder = new List<tb_login>();
            if (mgdata.Mgdatacount > 0)
            {
                foreach (DataRow dr in mgdata.Mgdata.Rows)
                {
                    tb_login cbai = new tb_login
                    {
                        id = Convert.ToInt32(dr["id"].ToString()),
                        UserName = dr["UserName"].ToString(),
                        Bank = dr["Bank"].ToString(),
                        Password = dr["Start_Date"].ToString(),
                        Tel =dr["End_Date"].ToString(),
                        Meeting = Convert.ToInt32(dr["Meeting"].ToString()),
                        added1 = dr["MeetingName"].ToString()

                    };
                    listorder.Add(cbai);
                };

            }
            ViewBag.recordCount = mgdata.Mgdatacount;
            ViewBag.ids = ids;
            ViewBag.pageindex = id ?? "0";
            return View(listorder);



        }
        /// <summary>
        /// 负责人导入会议数据
        /// </summary>
        /// <returns></returns>
        public ActionResult NewMeetingFzr()
        {
            return View();
        }
        /// <summary>
        /// 无权操作次功能
        /// </summary>
        /// <returns></returns>
        public ActionResult Nowork()
        {
            return View();
        }
        /// <summary>
        /// 会议统计 id 为会议 ids 为是否陌生
        /// </summary>
        /// <returns></returns>
      
        public ActionResult MeetingTJ(string id, string ids)
        {
            List<tb_gift> listorder = new List<tb_gift>();
            if (id != null && ids != null)
            {

                SqlParameter[] pms = { 
                                     new  SqlParameter("@meeting",id),
                                     new SqlParameter("@ms",ids)

                                     };
                MessasgeData mgdata = Datafun.MgfunctionData("Tj_rstj_poc", pms,"poc");        
                foreach (DataRow dr in mgdata.Mgdata.Rows)
                {
                    tb_gift cbai = new tb_gift
                    {
                      
                        UserName = dr["UserName"].ToString(),
                        Tel = dr["Tel"].ToString(),
                        Qdh = dr["Qdh"].ToString(),
                        Hongbao = dr["Hongbao"].ToString(),
                        Gift = dr["Gift"].ToString(),
                        Added1 = dr["sjrs"].ToString(),
                        Added2 = dr["yjrs"].ToString(),
                    };
                    listorder.Add(cbai);
                };
            }
            ViewBag.id = id;
            ViewBag.ids = ids;
            return View(listorder);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdatePassword()
        {
            return View();
        }
        /// <summary>
        /// 录入参会人员
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPerson()
        {
            return View();
        }
        #region 动作
        /// <summary>
        /// 导入数据源动作
        /// </summary>
        /// <returns></returns>
        public bool  DataInsertAjax(string filename,string meeting)
        {
            string dtTime = DateTime.Now.ToString(); ;
            //string filename = System.Web.HttpUtility.UrlDecode(Request["txt0"]);
          
            MessasgeInfor mgInfor = new MessasgeInfor
            {
                Mgbool = false,
                Mgtitle = "系统提示",
                Mgcontent = "操作失败，请确定上传文件是否正确(请参照模板)，或者文件是否有数据。"
            };
            //Datafun.Mgfunctioninfor(string.Format("update exl_slywh set del=1 where yearMonth='{0}'", dtTime));
            filename = Server.MapPath("~/Content/excel/" + filename);
            Excel_DR excelDr = new Excel_DR(filename,string.Format("select F1 as UserName,F2 as Tel1,F3 as Tel2 ,F4 as Znum,F5 as Yjrs,F6 as Bank   from  [Sheet1$] "), "");
            MessasgeData mgData = excelDr.GetData();


            SqlParameter[] parameters =
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
                new SqlParameter("@Tel1", SqlDbType.NVarChar, 50),
                new SqlParameter("@Tel2", SqlDbType.NVarChar, 50),
                new SqlParameter("@Znum", SqlDbType.NVarChar, 50),
                new SqlParameter("@Yjrs", SqlDbType.NVarChar, 50),
                new SqlParameter("@Bank", SqlDbType.NVarChar, 50),
                 new SqlParameter("@Meeting", SqlDbType.NVarChar, 50)
            };
            if (mgData.Mgdatacount > 0)
            {
                //DataTable dt1 = DataZDataActionDataTable(mgData.Mgdata, "yearMonth,userid", 0, mgData.Mgdatacount - 1, true, true, true, true, true);
                DataTable dt1 = Kain_class.HtmlSubstring.SubstringFunction.DataZDataActionDataTable(mgData.Mgdata, 1, mgData.Mgdatacount - 1, true, true, true, true, true);
                DataColumn priceColumn = new DataColumn();
                priceColumn.DataType = System.Type.GetType("System.Decimal");//该列的数据类型 
                priceColumn.ColumnName = "Meeting";//该列得名称 
                priceColumn.DefaultValue = meeting;//该列得默认值 
                dt1.Columns.Add(priceColumn); 
                mgInfor = Datafun.DataUpdate(dt1, "insert into tb_InsertMeeting(UserName,Tel1,Tel2,Znum,Yjrs,Bank,Meeting)values(@UserName,@Tel1,@Tel2,@Znum,@Yjrs,@Bank,@Meeting)", parameters);
                
            }

            else
            {
                mgInfor.Mgcontent = mgData.Mgcontent;
            }


            return mgInfor.Mgbool;
        }
        /// <summary>
        /// 上传客户源(excel)
        /// </summary>
        /// <param name="fileToUpload"></param>
        /// <returns></returns>
         [HttpPost]
        public string Upload(HttpPostedFileBase[] fileToUpload)
        {
            string Meeting = LgUser.Meetting;
            string Message = "<div style='text-align:center;font-size:100px;margin-top:400px'>上传失败</div>";
           
                foreach (HttpPostedFileBase file in fileToUpload)
                {
                    DateTime dtnow = System.DateTime.Now;
                    string FileName = dtnow.Year.ToString() + dtnow.Month.ToString() + dtnow.Day.ToString() + dtnow.Hour.ToString() + dtnow.Minute.ToString() + dtnow.Second.ToString() + dtnow.Millisecond.ToString();
                    string ExtName = getFileExt(file.FileName).ToUpper();
                    FileName += "." + ExtName;
                    string path = System.IO.Path.Combine(Server.MapPath("~/Content/excel"), System.IO.Path.GetFileName(FileName));
                    file.SaveAs(path);
                    SqlParameter[] pms = { 
                                      new SqlParameter("@MeetingName",Meeting),
                                      new SqlParameter("@excel",FileName)
                                      };
                    MessasgeData mgdata = Datafun.MgfunctionData(" update  tb_Meeting  set excel=@excel where id=@MeetingName  select  @@Identity", pms);
                    bool success = DataInsertAjax(FileName, Meeting);
                    if (success)
                    {
                        Message = "<div style='text-align:center;font-size:100px;margin-top:400px'>上传成功</div>";
                    }
                }


                return Message;
        }
        private string getFileExt(string fileName)
         {
             if (fileName.IndexOf(".") == -1)
                 return "";
             string[] temp = fileName.Split('.');
             return temp[temp.Length - 1].ToLower();
         }
        /// <summary>
        /// 添加负责人
        /// </summary>
        /// <returns></returns>
        public string AddfzrAjax()
        {

            string Meeting = Request["va1"];
            string UserName = Request["va2"];
            string Password = Request["va3"];
            string Start_Date = Request["va4"];
            string End_Date = Request["va5"];
            string tel = Request["va6"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",Meeting),
                                  new SqlParameter("@Bank",UserName),
                                    new SqlParameter("@Password",Passwd.SetPass(Password)) ,
                                      new SqlParameter("@Start_Date",Start_Date),
                                        new SqlParameter("@End_Date",End_Date),
                                         new SqlParameter("@UserName",tel) 
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("bs_NewMeeting_poc", pms,"poc");


            if (Convert.ToInt32(mgdata.Mgdata.Rows[0][0].ToString()) > 0)
            {
                IsSuccess = "0";
              
              
            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 签到号查询信息
        /// </summary>
        /// <returns></returns>
        public string SelectqdhAjax()
        {

            string qdh = Request["va1"];
            string Meeting = LgUser.Meetting;
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@qdh",qdh),
                                  new SqlParameter("@Meeting",Meeting)     
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData(@"select id,UserName,Tel1,Meeting from tb_InsertMeeting where qdh=@qdh and Meeting=@Meeting", pms);


            if (mgdata.Mgdatacount==1)
            {
                IsSuccess = mgdata.Mgdata.Rows[0]["UserName"] + " | " + mgdata.Mgdata.Rows[0]["Tel1"];


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 录入礼物红包
        /// </summary>
        /// <returns></returns>
        public string AddCashAjax()
        {

            string qdh = Request["va1"];
            string Meeting = LgUser.Meetting;
            string Hongbao = Request["va3"];
            string Gift = Request["va4"];
         
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Qdh",qdh),
                                  new SqlParameter("@Meeting",Meeting),
                                    new SqlParameter("@Hongbao",Hongbao),
                                      new SqlParameter("@Gift",Gift)     
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("bs_Gift_poc", pms,"poc");

                IsSuccess = mgdata.Mgdata.Rows[0][0].ToString();
          
            return IsSuccess;

        }
        /// <summary>
        /// 设置宴席数量（桌位人数）
        /// </summary>
        /// <returns></returns>
        public string AddZnumAjax()
        {
            
            string Meeting = LgUser.Meetting;
            int Znum = Convert.ToInt32( Request["va2"]);
            string Nums = Request["va3"];
            string Color = Request["va4"];
            string IsSuccess = "1";
            MessasgeInfor mginfor = new MessasgeInfor();

            for (int i = 1; i <= Znum; i++)
            {
                SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",Meeting),
                                  new SqlParameter("@Znum",Znum),
                                    new SqlParameter("@Nums",Nums),
                                      new SqlParameter("@Color",Color),       
                                        new SqlParameter("@Zh",i)  
                                        };
                mginfor = Datafun.Mgfunctioninfor(@"if not exists(select * from tb_znum where Meeting=@Meeting and isdel=0 and Zh=@Zh) begin insert into tb_znum (Meeting,Znum,Nums,Color,Zh) 
       values(@Meeting,@Znum,@Nums,@Color,@Zh) end", pms);

            
            }



           
            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }

            return IsSuccess;

        }
        /// <summary>
        /// 设置桌子名称
        /// </summary>
        /// <returns></returns>
        public string UznameAjax()
        {


            string id = Request["va1"];
            string Zname = Request["va2"];
            string IsSuccess = "1";
            MessasgeInfor mginfor = new MessasgeInfor();
                SqlParameter[] pms = {                          
                                 new SqlParameter("@id",id),
                                  new SqlParameter("@Zname",Zname)                              
                                        };
                mginfor = Datafun.Mgfunctioninfor(@"Update tb_znum set Zname=@Zname where id=@id", pms);       
            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }

            return IsSuccess;

        }
        /// <summary>
        /// 选择会议统计数据
        /// </summary>
        /// <returns></returns>
        public string SelectMeetingAjax()
        {

            string Meeting = Request["va1"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                              
                                  new SqlParameter("@Meeting",Meeting)     
                                        };
            MessasgeDataSet mgdataset = Datafun.MgfunctionDataSet(@"select COUNT(id) from tb_InsertMeeting where Meeting=@Meeting;select COUNT(id)  from tb_InsertMeeting where qdh <> 10000 and Meeting=@Meeting;select SUM(Hongbao) from tb_gift where Meeting=@Meeting;select COUNT(id)  from tb_InsertMeeting where qdh = 10000 and Meeting=@Meeting ", pms);



            IsSuccess = mgdataset.MgdataSet.Tables[0].Rows[0][0] + "|" + mgdataset.MgdataSet.Tables[1].Rows[0][0] + "|" + mgdataset.MgdataSet.Tables[2].Rows[0][0] + "|" + mgdataset.MgdataSet.Tables[3].Rows[0][0];


          
            return IsSuccess;

        }
        /// <summary>
        /// 设置会议游戏的开始时间和结束时间
        /// </summary>
        /// <returns></returns>
        public string SetGameAjax()
        {

            string Meeting = LgUser.Meetting;         
            string Start_Date = Request["va2"];
            string End_Date = Request["va3"];
         
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",Meeting),                              
                                      new SqlParameter("@Start_Date",Start_Date),
                                        new SqlParameter("@End_Date",End_Date),                                      
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"if not exists(select * from tb_GameSet where Meeting=@Meeting and isdel=0) begin insert into tb_GameSet (Meeting,Start_Date,End_Date) 
      values(@Meeting,@Start_Date,@End_Date) end", pms);


            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 最高负责人录入新会议
        /// </summary>
        /// <returns></returns>
        public string AddMeetingAjax()
        {

            string Meeting = Request["va1"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",Meeting)
                                
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"if not exists(select * from tb_Meeting where MeetingName=@Meeting and isdel=0) begin insert into tb_Meeting (MeetingName) 
      values(@Meeting) end", pms);


            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 录入参会人员
        /// </summary>
        /// <returns></returns>
        public string AddchAjax()
        {

            string UserName = Request["va1"];
            string tel1 = Request["va2"];
            string tel2 = Request["va3"];
            string znum = Request["va4"];
            string yjrs = Request["va5"];
            string Bank = Request["va6"];
            string Meeting = LgUser.Meetting;
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",Meeting),
                                  new SqlParameter("@UserName",UserName),
                                    new SqlParameter("@Tel1",tel1) ,
                                      new SqlParameter("@Tel2",tel2),
                                        new SqlParameter("@znum",znum),
                                         new SqlParameter("@yjrs",yjrs),
                                          new SqlParameter("@Bank",Bank) 
                                        
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor("insert into tb_InsertMeeting(UserName,Tel1,Tel2,znum,yjrs,Bank,Meeting)values(@UserName,@Tel1,@Tel2,@znum,@yjrs,@Bank,@Meeting)", pms);


            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 验证手机
        /// </summary>
        /// <returns></returns>
        public string Ytel()
        {

            string Meeting = LgUser.Meetting;
            string tel = Request["va1"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                    new SqlParameter("@Meeting",Meeting),
                                         new SqlParameter("@tel",tel) 
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("select * from tb_InsertMeeting where Meeting=@Meeting and(Tel1=@tel or Tel2=@tel)", pms);


            if (mgdata.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 验证会议是否存在
        /// </summary>
        /// <returns></returns>
        public string Ymeeting()
        {

         
            string MeetingName = Request["va1"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                            
                                         new SqlParameter("@MeetingName",MeetingName) 
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("select * from tb_Meeting where MeetingName=@MeetingName", pms);


            if (mgdata.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string UpdatePassWordAjax()
        {

            string UserID = LgUser.UserId;
            string password = Request["va1"];
       

            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@UserID",UserID),                              
                                      new SqlParameter("@password",Passwd.SetPass(password))
                                                                       
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"update tb_login set Password=@password where id=@UserID", pms);


            if (mginfor.Mgdatacount > 0)
            {
                IsSuccess = "0";


            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 宴会统计导出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <summary>
        /// 宴会统计导出（弃用）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileResult ExportExcel(string id,string ids)
        {
           
            Excel_DC exdr = new Excel_DC();
            FileResult sResult = null;
            MessasgeData mgdata = new MessasgeData();
            StringBuilder sbr = new StringBuilder();
            Hashtable hx = new Hashtable();
            HSSFWorkbook hswork = new HSSFWorkbook();
            SqlParameter[] pms = { 
                                     new  SqlParameter("@meeting",id),
                                     new SqlParameter("@ms",ids)

                                     };
            mgdata = Datafun.MgfunctionData("select MeetingName from tb_meetting where id=@meeting", pms);
            string meetname = "";
            if (mgdata.Mgdatacount > 0)
            {
                meetname = mgdata.Mgdata.Rows[0][0].ToString();
            }
            mgdata = Datafun.MgfunctionData("Tj_rstj_poc", pms, "poc");


            List<Exlhead> exlist = new List<Exlhead>();
            string titlename = meetname + "统计表";
            if (mgdata.Mgdatacount > 0)
            {

                #region


                string title = meetname + "统计表";
                string workname = "会议统计表";
                Exlhead exlhead = new Exlhead(mgdata.Mgdata, 2, title, workname, hswork);
                exlhead.CellStyleInit();
                exlhead.CreaTitle(7);
                exlhead.ISheet.SetColumnWidth(0, 80 * 50);
                exlhead.ISheet.SetColumnWidth(1, 80 * 50);
                exlhead.ISheet.SetColumnWidth(2, 80 * 40);
                exlhead.ISheet.SetColumnWidth(3, 80 * 35);
                exlhead.ISheet.SetColumnWidth(4, 80 * 35);
                exlhead.ISheet.SetColumnWidth(5, 80 * 100);
                exlhead.ISheet.SetColumnWidth(6, 80 * 35);
                exlhead.ISheet.SetColumnWidth(7, 80 * 35);





                //设置标题
                IRow iRow = exlhead.ISheet.CreateRow(1);
                ICell iCell = iRow.CreateCell(0);
                iCell.SetCellValue("姓名");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(1);
                iCell.SetCellValue("电话");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(2);
                iCell.SetCellValue("签到号");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(3);
                iCell.SetCellValue("红包");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(4);
                iCell.SetCellValue("礼物");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(5);
                iCell.SetCellValue("实际人数");
                iCell.CellStyle = exlhead.CellthendStyle;
                iCell = iRow.CreateCell(6);
                iCell.SetCellValue("预计人数");
                iCell.CellStyle = exlhead.CellthendStyle;






                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 7));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 0, 0));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 1, 1));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 2, 2));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 3, 3));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 4, 4));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 5, 5));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 6, 6));
                exlhead.ISheet.AddMergedRegion(new CellRangeAddress(1, 2, 7, 7));


                sResult = exdr.ExlActionMvc(title, exlhead);


                exlist.Add(exlhead);


            }
                #endregion


            //设置表头


            sResult = exdr.ExlActionMvc(titlename, exlist, hswork);
          
            return sResult;
        }
        /// <summary>
        /// NPOI 宴会统计导出
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ids"></param>
        public void Export(string id,string ids)
        {
           
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
          

            NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = workbook.CreateSheet("宴会统计");

            //excel格式化
            NPOI.SS.UserModel.ICellStyle dateStyle = workbook.CreateCellStyle();
            dateStyle.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/m/d h:mm:ss");

            NPOI.SS.UserModel.ICellStyle numberStyle = workbook.CreateCellStyle();
            numberStyle.DataFormat = workbook.CreateDataFormat().GetFormat("0.00000");

            NPOI.SS.UserModel.ICellStyle textStyle = workbook.CreateCellStyle();
            textStyle.DataFormat = workbook.CreateDataFormat().GetFormat("@");
           
            //设置单元格宽度
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
          
          

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("姓名");
            row1.CreateCell(1).SetCellValue("电话号码");
            row1.CreateCell(2).SetCellValue("签到号");
            row1.CreateCell(3).SetCellValue("红包");
            row1.CreateCell(4).SetCellValue("礼物");
            row1.CreateCell(5).SetCellValue("实际人数");
            row1.CreateCell(6).SetCellValue("预计人数");

          
            //设置列宽
            row1.Sheet.SetColumnWidth(0, 80 * 50);
            row1.Sheet.SetColumnWidth(1, 80 * 50);
            row1.Sheet.SetColumnWidth(2, 50 * 50);
            row1.Sheet.SetColumnWidth(3, 80 * 50);
            row1.Sheet.SetColumnWidth(4, 150 * 50);
            row1.Sheet.SetColumnWidth(5, 80 * 50);
            row1.Sheet.SetColumnWidth(6, 80 * 50);
          
           
            MessasgeData mgdata=new MessasgeData();
            SqlParameter[] pms = { 
                                     new  SqlParameter("@meeting",id),
                                     new SqlParameter("@ms",ids)

                                     };
            mgdata = Datafun.MgfunctionData("select MeetingName from tb_Meeting where id=@meeting", pms);
            if (mgdata.Mgdatacount > 0)
            {
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "" + mgdata.Mgdata.Rows[0][0].ToString() + ".xlsx"));//添加Excel名字
            }
            else
            {
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "宴会统计.xlsx"));//添加Excel名字
            }
               mgdata = Datafun.MgfunctionData("Tj_rstj_poc", pms, "poc");
           for (int i = 0; i < mgdata.Mgdata.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);

              
                rowtemp.CreateCell(0).SetCellValue(mgdata.Mgdata.Rows[i][0].ToString());
                rowtemp.CreateCell(1).SetCellValue(mgdata.Mgdata.Rows[i][1].ToString());
                rowtemp.CreateCell(2).SetCellValue(mgdata.Mgdata.Rows[i][2].ToString());
                rowtemp.CreateCell(3).SetCellValue(mgdata.Mgdata.Rows[i][3].ToString());
                rowtemp.CreateCell(4).SetCellValue(mgdata.Mgdata.Rows[i][4].ToString());
                rowtemp.CreateCell(5).SetCellValue(mgdata.Mgdata.Rows[i][5].ToString());
                rowtemp.CreateCell(6).SetCellValue(mgdata.Mgdata.Rows[i][6].ToString());

            
               
               
           

                rowtemp.GetCell(0).CellStyle = textStyle;
                rowtemp.GetCell(1).CellStyle = textStyle;
                rowtemp.GetCell(2).CellStyle = textStyle;
                rowtemp.GetCell(3).CellStyle = numberStyle;
                rowtemp.GetCell(4).CellStyle = textStyle;
                rowtemp.GetCell(5).CellStyle = textStyle;
                rowtemp.GetCell(6).CellStyle = numberStyle;
               
               
            }
            //写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            workbook.Write(ms);
            Response.BinaryWrite(ms.ToArray());

            Response.Flush();
            Response.End();
        }
        /// <summary>
        /// NPOI 导出会议的管理人员
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ids"></param>
        public void ExportMeeting()
        {

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";


            NPOI.XSSF.UserModel.XSSFWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet1 = workbook.CreateSheet("宴会统计");

            //excel格式化
            NPOI.SS.UserModel.ICellStyle dateStyle = workbook.CreateCellStyle();
            dateStyle.DataFormat = workbook.CreateDataFormat().GetFormat("yyyy/m/d h:mm:ss");

            NPOI.SS.UserModel.ICellStyle numberStyle = workbook.CreateCellStyle();
            numberStyle.DataFormat = workbook.CreateDataFormat().GetFormat("0.00000");

            NPOI.SS.UserModel.ICellStyle textStyle = workbook.CreateCellStyle();
            textStyle.DataFormat = workbook.CreateDataFormat().GetFormat("@");

            //设置单元格宽度
            NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();



            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("姓名");
            row1.CreateCell(1).SetCellValue("电话号码");
            row1.CreateCell(2).SetCellValue("会议名称");
            row1.CreateCell(3).SetCellValue("开始时间");
            row1.CreateCell(4).SetCellValue("结束时间");
           


            //设置列宽
            row1.Sheet.SetColumnWidth(0, 100 * 50);
            row1.Sheet.SetColumnWidth(1, 100 * 50);
            row1.Sheet.SetColumnWidth(2, 150 * 50);
            row1.Sheet.SetColumnWidth(3, 100 * 50);
            row1.Sheet.SetColumnWidth(4, 100 * 50);
            


            MessasgeData mgdata = new MessasgeData();
            mgdata = Datafun.MgfunctionData("select Bank,UserName,b.MeetingName,convert(nvarchar(11),Start_Date,120) ,convert(nvarchar(11),End_Date,120) from tb_login a left join tb_Meeting b on a.Meeting=b.id where Lvl=1 and a.isdel=1 ");
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", "会议管理人员.xlsx"));//添加Excel名字
        
            for (int i = 0; i < mgdata.Mgdata.Rows.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);


                rowtemp.CreateCell(0).SetCellValue(mgdata.Mgdata.Rows[i][0].ToString());
                rowtemp.CreateCell(1).SetCellValue(mgdata.Mgdata.Rows[i][1].ToString());
                rowtemp.CreateCell(2).SetCellValue(mgdata.Mgdata.Rows[i][2].ToString());
                rowtemp.CreateCell(3).SetCellValue(mgdata.Mgdata.Rows[i][3].ToString());
                rowtemp.CreateCell(4).SetCellValue(mgdata.Mgdata.Rows[i][4].ToString());
            






                rowtemp.GetCell(0).CellStyle = textStyle;
                rowtemp.GetCell(1).CellStyle = textStyle;
                rowtemp.GetCell(2).CellStyle = textStyle;
                rowtemp.GetCell(3).CellStyle = numberStyle;
                rowtemp.GetCell(4).CellStyle = textStyle;
              


            }
            //写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            workbook.Write(ms);
            Response.BinaryWrite(ms.ToArray());

            Response.Flush();
            Response.End();
        }
        #endregion
    }
}
