using Dinner.Models;
using Dinner.Models.Filter;
using Kain_class.Messageinfor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dinner.Controllers
{
    public class GuestController :MainsController
    {
        #region 视图
        /// <summary>
        /// 首页  未到人数就为录入进去 未签到的人数
        /// </summary>
        /// <returns></returns>
        [LoginFilter(Orderid = 1)]
        public ActionResult Index(string id,string ids)
        {
            MessasgeData mgdata = new MessasgeData();
            ViewBag.tel = id ?? "0";
            string sql="";
            if (LgUser.UserLvl==0)
            {
                return RedirectToAction("AdminIndex","Home") ;
                
            }
            ViewBag.message = "";
            SqlParameter[] pms = { 
                                 new SqlParameter("@meeting",LgUser.Meetting),
                                  new SqlParameter("@tel",id??"")
                                 };
            mgdata = Datafun.MgfunctionData(@"select sjrs=(select top 1 sjrs from  dbo.tb_InsertMeeting where meeting=@meeting and (Tel1=@tel or Tel2=@tel)) ,Sj=(select isnull(sum(CONVERT(int,isnull(sjrs,0))),0) from  dbo.tb_InsertMeeting where meeting=@meeting ),
Yj=(select isnull(COUNT(id),0)  from  dbo.tb_InsertMeeting where meeting=@meeting and qdh=10000)", pms);
            if (mgdata.Mgdatacount > 0)
            {
                if (ids == "需要选桌")
                {
                    ViewBag.message = "有" + mgdata.Mgdata.Rows[0][0].ToString() + "位人数需要选择座位";
                    sql = "select id, Nums,(Nums-Yzrs)as syrs,Color,Zh,Zname from tb_znum where Meeting=" + LgUser.Meetting + " and (Nums-Yzrs)>=" + mgdata.Mgdata.Rows[0][0].ToString() + "";
                }
                else
                {
                    ViewBag.message = "无需选择座位";
                       sql="select id, Nums,(Nums-Yzrs)as syrs,Color,Zh,Zname from tb_znum where Meeting="+LgUser.Meetting+"";
                }
                ViewBag.Yj = mgdata.Mgdata.Rows[0]["Yj"].ToString();
                ViewBag.Sj = mgdata.Mgdata.Rows[0]["Sj"].ToString();
            }
            mgdata = Datafun.MgfunctionData(sql);
            List<tb_Znum> listorder = new List<tb_Znum>();
            foreach (DataRow dr in mgdata.Mgdata.Rows)
            {
                tb_Znum cbai = new tb_Znum
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    Nums = dr["Nums"].ToString(),
                    Yzrs = dr["syrs"].ToString(),
                    Color = dr["Color"].ToString(),
                    Zh =Convert.ToInt32(dr["Zh"].ToString()),
                    Zname = dr["Zname"].ToString()
                   
                };
                listorder.Add(cbai);
                ViewBag.znum = dr["Nums"].ToString();
                ViewBag.color = dr["Color"].ToString();
            };


            return View(listorder);
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginFilter(Orderid = 1)]
        public ActionResult IndexNofoot(string id, string ids)
        {
            MessasgeData mgdata = new MessasgeData();
            ViewBag.tel = id;
            string sql = "";
            if (LgUser == null)
            {
                return View("Sign");

            }
            ViewBag.message = "";
            SqlParameter[] pms = { 
                                 new SqlParameter("@meeting",LgUser.Meetting),
                                  new SqlParameter("@tel",id??"")
                                 };
            mgdata = Datafun.MgfunctionData(@"select sjrs=(select top 1 sjrs from  dbo.tb_InsertMeeting where meeting=@meeting and (Tel1=@tel or Tel2=@tel)) ,Sj=(select isnull(sum(CONVERT(int,isnull(sjrs,0))),0) from  dbo.tb_InsertMeeting where meeting=@meeting ),
Yj=(select isnull(sum(CONVERT(int,isnull(yjrs,0))),0) from  dbo.tb_InsertMeeting where meeting=@meeting)", pms);
            if (mgdata.Mgdatacount > 0)
            {
                if (ids == "需要选桌")
                {
                    ViewBag.message = "有" + mgdata.Mgdata.Rows[0][0].ToString() + "位人数需要选择座位";
                    sql = "select id, Nums,(Nums-Yzrs)as syrs,Color,Zh,Zname from tb_znum where Meeting=" + LgUser.Meetting + " and (Nums-Yzrs)>=" + mgdata.Mgdata.Rows[0][0].ToString() + "";
                }
                else
                {
                    ViewBag.message = "无需选择座位";
                    sql = "select id, Nums,(Nums-Yzrs)as syrs,Color,Zh,Zname from tb_znum where Meeting=" + LgUser.Meetting + "";
                }
                ViewBag.Yj = Convert.ToInt32( mgdata.Mgdata.Rows[0]["Yj"].ToString()) -Convert.ToInt32( mgdata.Mgdata.Rows[0]["Sj"].ToString());
                ViewBag.Sj = mgdata.Mgdata.Rows[0]["Sj"].ToString();
            }
            mgdata = Datafun.MgfunctionData(sql);
            List<tb_Znum> listorder = new List<tb_Znum>();
            foreach (DataRow dr in mgdata.Mgdata.Rows)
            {
                tb_Znum cbai = new tb_Znum
                {
                    id = Convert.ToInt32(dr["id"].ToString()),
                    Nums = dr["Nums"].ToString(),
                    Yzrs = dr["syrs"].ToString(),
                    Color = dr["Color"].ToString(),
                    Zh = Convert.ToInt32(dr["Zh"].ToString()),
                    Zname = dr["Zname"].ToString()
                };
                listorder.Add(cbai);
                ViewBag.znum = dr["Nums"].ToString();
                ViewBag.color = dr["color"].ToString();
            };


            return View(listorder);
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public ActionResult Sign()
        {
            return View();
        }
        /// <summary>
        /// 签到信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SignInfor()
        {
            //if (LgUser != null)
            //{
                
            //    return RedirectToAction("SignOver", "Guest");
            //}
            return View();
        }
        /// <summary>
        /// 新增嘉宾
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGuest(string id,string ids)
        {
            ViewBag.tel = id;
            ViewBag.sjrs = ids;
            return View();
        }
        /// <summary>
        /// 签到成功
        /// </summary>
        /// <returns></returns>
        public ActionResult SignSuccess(string id,string ids)
        {

            if (ids == "需要选桌")
            {

                return RedirectToAction("Index", "Guest", new { id = id, ids = "需要选桌" });
            
            }

            SqlParameter[] pms = { 
                               new SqlParameter("@meeting",LgUser.Meetting),
                               new SqlParameter("@tel",id)

                                 };

            MessasgeData mgdata = Datafun.MgfunctionData(" select qdh,znum,UserName,Bank,Meeting,id,sjrs from    tb_InsertMeeting  where  meeting=@meeting and  (Tel1=@tel or Tel2=@tel)", pms);
            if (mgdata.Mgdatacount > 0)
            {
                ViewBag.znum = mgdata.Mgdata.Rows[0]["znum"];
                ViewBag.qdh = mgdata.Mgdata.Rows[0]["qdh"];
               
              
            }
            ViewBag.tel = id;
            ViewBag.ids = ids;
            return View();
        }
        /// <summary>
        /// 签到后赠送礼物
        /// </summary>
        /// <returns></returns>
        public ActionResult Signgift(string id,string ids)
        {

            SqlParameter[] pms = { 
                               new SqlParameter("@meeting",LgUser.Meetting),
                               new SqlParameter("@tel",id)

                                 };

            MessasgeData mgdata = Datafun.MgfunctionData(" select qdh,znum,UserName,Bank,Meeting,id,sjrs from    tb_InsertMeeting  where  meeting=@meeting and  (Tel1=@tel or Tel2=@tel)", pms);
            if (LgUser != null)
            {
                ViewBag.qdh = mgdata.Mgdata.Rows[0]["qdh"];
                ViewBag.znum = mgdata.Mgdata.Rows[0]["znum"];
               
                ViewBag.name = mgdata.Mgdata.Rows[0]["UserName"];
            }
            ViewBag.tel = id;
            ViewBag.ids = ids;
            return View();
        }
        /// <summary>
        /// 已经签到
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOver(string id)
        {  
            
            SqlParameter[] pms = { 
                               new SqlParameter("@meeting",LgUser.Meetting),
                               new SqlParameter("@tel",id)

                                 };

            MessasgeData mgdata = Datafun.MgfunctionData(" select qdh,znum,UserName,Bank,Meeting,id,sjrs from    tb_InsertMeeting  where  meeting=@meeting and  (Tel1=@tel or Tel2=@tel)",pms);
            if (mgdata.Mgdatacount > 0)
            {
                ViewBag.UserName = mgdata.Mgdata.Rows[0]["UserName"];
                ViewBag.qdh = mgdata.Mgdata.Rows[0]["qdh"];
                ViewBag.sjrs = mgdata.Mgdata.Rows[0]["sjrs"];
                ViewBag.Meeting = mgdata.Mgdata.Rows[0]["Meeting"];
            }
            ViewBag.tel = LgUser.Tel;
            return View();
        }

        /// <summary>
        /// 选桌子ids 为电话号码
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangeDesk(string id, string ids,string idss)
        {
            ViewBag.message = idss;
            
            try
            {
               
                ViewBag.name = LgUser.UserName;
                ViewBag.nums = "0";
                ViewBag.zh = id;
                ViewBag.tel = ids;
                string sb = "";
                string sc = "";
                MessasgeDataSet mgdatadset = new MessasgeDataSet();
                if (id == null)
                {
                    return Content("操作有误，规范操作");
                }
                string meeting = LgUser.Meetting;

                SqlParameter[] pms ={
                              new SqlParameter("@meeting",meeting),
                                new SqlParameter("@znum",id),
                                 new SqlParameter("@tel",ids)
                              };
                mgdatadset = Datafun.MgfunctionDataSet(@"select UserName,Sjrs 
                                                from  tb_InsertMeeting where Status=1 and Meeting=@meeting and Znum=@znum;select isnull(nums,0),color from tb_znum where Meeting=@meeting and Zh=@znum;select ISNULL( SUM(CONVERT(int,sjrs)),0) from  tb_InsertMeeting where Status=1 and Meeting=@meeting and Znum=@znum;select qdh
                                                from  tb_InsertMeeting where  Meeting=@meeting and(Tel1=@tel or Tel2=@tel)", pms);

                ViewBag.nums = mgdatadset.MgdataSet.Tables[1].Rows[0][0];
                ViewBag.color = mgdatadset.MgdataSet.Tables[1].Rows[0][1];
                ViewBag.qdh = "";
                if (mgdatadset.MgdataSet.Tables[3].Rows.Count > 0)
                {
                    ViewBag.qdh = mgdatadset.MgdataSet.Tables[3].Rows[0][0];
                }
                List<string> user = new List<string>();
                List<string> userxiaoyu = new List<string>();

                foreach (DataRow dr in mgdatadset.MgdataSet.Tables[0].Rows)
                {
                    for (int i = 0; i < Convert.ToInt32(dr["Sjrs"].ToString()); i++)
                    {
                        user.Add(dr["UserName"].ToString());
                        userxiaoyu.Add(dr["UserName"].ToString());
                    }
                }

                int less = Convert.ToInt32(ViewBag.nums) - Convert.ToInt32(mgdatadset.MgdataSet.Tables[2].Rows[0][0].ToString());//剩余空位
                int zhless = Convert.ToInt32(ViewBag.nums) - 10;//圆盘额外的座位
                //string [] names={"关羽","岳飞","成龙","奥巴马","金龙鱼","栗色","王晓","宋江","李师师"};
                //string[] namestt = { "李自成", "孝庄", "陈圆圆" };
                if (Convert.ToInt32(ViewBag.nums) >= 10)//桌子设置大于10
                {

                    if (user.Count > 10)//来宾人数是否大于10
                    {
                        userxiaoyu.RemoveRange(0, 10);
                        user.RemoveRange(10, user.Count - 10);
                        sc = dazxAjax(zhless, less, userxiaoyu);//大于10个 来的人数-10 判断3 是 小于等于10 减下来的
                        sb = xiaozxAjax(10, 10, user);//小于10个 记得判断人数大于10小于10个
                    }
                    else
                    {
                        sc = ttAjax(zhless);//大于10个 来的人数-10 判断3 是 小于等于10 减下来的 圆盘下面的
                        sb = cczxAjax(10, zhless, user);//小于10个 记得判断人数大于10小于10个 圆盘

                    }

                }
                else//桌子设置小于10
                {

                    sb = xiaozxAjax(Convert.ToInt32(ViewBag.nums), less, user);

                }
                ViewBag.sb = sb;
                ViewBag.sc = sc;
            }
            catch(Exception e)
            {
            return Content("选桌错误,请联系工作人员");
            }
            return View();
        }
        #endregion
        #region 动作
        #region 选桌展示
        /// <summary>
        /// 选桌子大于10个人
        /// </summary>
        /// <returns></returns>
        public string dazxAjax(int nums,int sjrs,List<string> name)
        {

            string id = Request["va1"];
            string ids = Request["va2"];
            string bsid = id;
            int namecount = name.Count;


            StringBuilder sb = new StringBuilder();
            string scb = "";
            string sql = "";
            string sli = "";
             string sliew = "";

         

            sli = @" 
                   <li><a href='#'>
        <img src='/Content/images/desk_peo_1.png' />
        <div>{$UserName$}</div>
      </a></li>";
       sliew = @" 
           <li onclick='clilks()'><a href='#'>
        <img src='/Content/images/desk_peo.png' />
        <div class='hidden'>笑嘻嘻</div>
      </a></li>";

       foreach (string username in name)
       {
           scb = sli.Replace("{$UserName$}", username);
           sb.Append(scb);
         
       }
       for (int i = 1; i <=sjrs; i++)
       {

           sb.Append(sliew);
       }









            return sb.ToString();
        }
        /// <summary>
        /// 选桌子小于10个人
        /// </summary>
        /// <returns></returns>
        public string xiaozxAjax(int nums, int sjrs, List<string> name)
        {
            
              int p = 1;
            string id = Request["va1"];
            string ids = Request["va2"];
            string bsid = id;
            int namecount = name.Count;


            StringBuilder sb = new StringBuilder();
            string scb = "";
            string sql = "";
            string sli = "";
            string sliew = "";

          
            sli = @" 
                    <li class='li{$i$}'><a href='#'>
        <div class='text'>{$UserName$}</div>
        <img src='/Content/images/desk_peo_1.png' />
      </a></li>";

              sliew = @" 
                    <li class='li{$j$}' onclick='clilks()'><a href='#'>
        <div class='text hidden'>笑嘻嘻</div>
        <img src='/Content/images/desk_peo.png' />
      </a></li> ";

            foreach (string username in name)
            {
                scb = sli.Replace("{$UserName$}", username).Replace("{$i$}",p.ToString());
                sb.Append(scb);
                p++;
            }
            for (int i = name.Count+1; i <= nums; i++)
            {
                sql = sliew.Replace("{$j$}", i.ToString());
                sb.Append(sql);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 选桌子大于10个来宾人数小于10
        /// </summary>
        /// <returns></returns>
        public string cczxAjax(int nums, int sjrs, List<string> name)
        {

            int p = 1;
            string id = Request["va1"];
            string ids = Request["va2"];
            string bsid = id;
            int namecount = name.Count;


            StringBuilder sb = new StringBuilder();
            string scb = "";
            string sql = "";
            string sli = "";
            string sliew = "";
         
          
            sli = @" 
                    <li class='li{$i$}'><a href='#'>
        <div class='text'>{$UserName$}</div>
        <img src='/Content/images/desk_peo_1.png' />
      </a></li>";

            sliew = @" 
                  <li class='li{$j$}' onclick='clilks()'><a href='#'>
        <div class='text hidden'>笑嘻嘻</div>
        <img src='/Content/images/desk_peo.png' />
      </a></li> ";

        
            int ypkw = 10 - name.Count;

            foreach (string username in name)
            {
                scb = sli.Replace("{$UserName$}", username).Replace("{$i$}", p.ToString());
                sb.Append(scb);
                p++;
            }
            for (int i = name.Count+1; i <= 10; i++)
            {
                sql = sliew.Replace("{$j$}", i.ToString());
                sb.Append(sql);
            }
          
            return sb.ToString();
        }
        /// <summary>
        /// 选桌大于10人来宾不够10人 圆盘下面都是空位
        /// </summary>
        /// <returns></returns>
        public string ttAjax(int nums)
        {

            StringBuilder sb = new StringBuilder();
            string sliew = "";      
            sliew = @" 
           <li onclick='clilks()'><a href='#'>
        <img src='/Content/images/desk_peo.png' />
        <div class='hidden'>笑嘻嘻</div>
      </a></li>";

           
            for (int i = 1; i <= nums; i++)
            {

                sb.Append(sliew);
            }
            return sb.ToString();
        }
        #endregion
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns></returns>
        public string AddSignAjax()
        {  
            
            string meeting = LgUser.Meetting;
            string tel = Request["va2"];
            string sjrs = Request["va1"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@meeting",meeting),
                                  new SqlParameter("@tel",tel),
                                  new SqlParameter("@sjrs",sjrs) 
                                        };
            MessasgeDataSet mgdataset = Datafun.MgfunctionDataSet("bs_sign_poc", pms, "poc");


            if (mgdataset.MgdataSet.Tables[1].Rows[0][0].ToString()=="0")
            {
              
                //DataRow dr = mgdataset.MgdataSet.Tables[0].Rows[0];
                //LoginUser lguser = new LoginUser
                //{
                //    UserId = dr["id"].ToString(),
                //    UserName = dr["UserName"].ToString(),
                //    qdh = dr["qdh"].ToString(),
                //    znum =dr["znum"].ToString(),
                //    Bank = dr["Bank"].ToString(),
                //    Tel = dr["tel"].ToString(),
                //    Meetting = dr["Meeting"].ToString()
                //};
                //Session["Lguser"] = lguser;
                IsSuccess = "0"+"|"+mgdataset.MgdataSet.Tables[0].Rows[0][0].ToString();
            }
            else if (mgdataset.MgdataSet.Tables[1].Rows[0][0].ToString() == "1")
            {

                IsSuccess = "1" + "|" + mgdataset.MgdataSet.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                IsSuccess = "2" + "|" + mgdataset.MgdataSet.Tables[0].Rows[0][0].ToString();
            }
            return IsSuccess;

        }
        /// <summary>
        /// 赠送礼物
        /// </summary>
        /// <returns></returns>
        public string AddGiftAjax()
        {
            string gift = Request["va1"];
           
            string tel = Request["va3"];
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@gift",gift),
                                
                                   new SqlParameter("@meeting",LgUser.Meetting),
                                    new SqlParameter("@tel",tel) 
                                        };

            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(" update tb_InsertMeeting set gift=@gift where meeting=@meeting and(Tel1=@tel or Tel2=@tel)",pms);

            if (mginfor.Mgdatacount>0)
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
        /// 新增嘉宾
        /// </summary>
        /// <returns></returns>
        public string AddGuestAjax()
        {
            string gift = Request["va1"];
            string tel = Request["va2"];
            string UserName = Request["va3"];
            string sjrs = Request["va4"];
            string bank = Request["va5"];
            string meeting = LgUser.Meetting;
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@gift",gift),
                                  new SqlParameter("@tel",tel),
                                  new SqlParameter("@UserName",UserName),
                                  new SqlParameter("@sjrs",sjrs),
                                  new SqlParameter("@bank",bank),
                                 new SqlParameter("@meeting",meeting)
                                        };

            MessasgeData mgdata = Datafun.MgfunctionData("bs_AddGuest_poc", pms,"poc");

            if (mgdata.Mgdatacount > 0)
            {
                DataRow dr = mgdata.Mgdata.Rows[0];
                LoginUser lguser = new LoginUser
                {
                    UserId = dr["id"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    qdh = dr["qdh"].ToString(),
                    znum = dr["znum"].ToString(),
                    Bank = dr["Bank"].ToString(),
                    Tel = dr["Tel1"].ToString(),
                    Meetting = dr["Meeting"].ToString()
                };
                Session["Lguser"] = lguser;

                IsSuccess = "0";
            }
            else
            {

                IsSuccess = "1";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 选桌子
        /// </summary>
        /// <returns></returns>
        public string deskAjax()
        {
            string tel = Request["va1"];
            string znum = Request["va2"];    
            string meeting = LgUser.Meetting;
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@tel",tel),
                                  new SqlParameter("@znum",znum),
                                  new SqlParameter("@meeting",meeting),                              
                                        };

            MessasgeData mgdata = Datafun.MgfunctionData("bs_xuanzhuo_poc", pms, "poc");

         
                IsSuccess = mgdata.Mgdata.Rows[0][0].ToString();
              
            return IsSuccess;

        }
        #endregion
    }
}
