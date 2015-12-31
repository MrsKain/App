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
    public class GameController : MainsController
    {
        #region 视图
        /// <summary>
        /// 点击游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult Clicks()
        {
            if (LgGameUser == null)
            {
                return RedirectToAction("LoginGame", "Game");
            }
            ViewBag.Starttime = "";
            MessasgeData mgdata = Datafun.MgfunctionData("select * from tb_GameSet where Meeting=@id and status=1", new SqlParameter("@id", LgGameUser.Meeting));
            if (mgdata.Mgdatacount > 0)
            {

                return View();


            }
            else
            {
                return RedirectToAction("GameNostart","Game");
            }
          
        }
       
        /// 抽奖
        /// </summary>
        /// <returns></returns>
        [LoginFilter(Orderid = 1)]
        public ActionResult Choujiang()
        {
            return View();
        }
        /// <summary>
        /// 5 4 3 2 1 
        /// </summary>
        /// <returns></returns>
        public ActionResult Nums()
        {
            if (LgGameUser == null)
            {
                return RedirectToAction("LoginGame", "Game");
            }
            return View();
        }
        /// <summary>
        /// 游戏未开始界面
        /// </summary>
        /// <returns></returns>
        public ActionResult GameNostart()
        {
            if (LgGameUser == null)
            {
                return RedirectToAction("LoginGame", "Game");
            }
            ViewBag.Starttime = "";
            MessasgeData mgdata = Datafun.MgfunctionData("select * from tb_GameSet where Meeting=@id and status=1", new SqlParameter("@id", LgGameUser.Meeting));
            if (mgdata.Mgdatacount > 0)
            {

                return View("Nums");


            }
            return View();
        }
        /// <summary>
        /// 游戏进入页面（输入签到的电话号码）
        /// </summary>
        /// <returns></returns>
        public ActionResult Timestart()
        {
            return View();
        }
        /// <summary>
        /// 登录游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginGame(string id)
        {
            ViewBag.ids = id;
            ViewBag.MeetingName = "会议选择有错误,点击取消重新选择会议！";
            MessasgeData mgdata = Datafun.MgfunctionData("select MeetingName from  tb_Meeting where id=@Meeting", new SqlParameter("@Meeting", id));
            if (mgdata.Mgdatacount > 0)
            {
                ViewBag.MeetingName = "是否进入" + mgdata.Mgdata.Rows[0][0] + "游戏"; ;
            }
            return View();
        }
        /// <summary>
        /// 选择游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGame()
        {
            
            return View();
        }
        /// <summary>
        /// 评论墙游戏
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentGame()
        {
            if (LgGameUser == null)
            {
            
                return RedirectToAction("LoginGame","Game");
            }
            return View();
        }
        /// <summary>
        /// 评论墙列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentList()
        {
            return View();
        }
        /// <summary>
        /// 结果页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Result()
        {
            return View();
        }
        /// <summary>
        /// 游戏结果页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GameResult()
        {
            if (LgUser == null)
            {
                return RedirectToAction("Login","Login");
            }

            MessasgeData mgdata = Datafun.MgfunctionData(@"select* from tb_clicks where Meeting=@Meeting and isdel=0 order by  Nums  desc", new SqlParameter("@Meeting", LgUser.Meetting));
            ViewBag.table = mgdata.Mgdata;
            return View();
        }
        /// <summary>
        /// 评论列表
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentResult()
        {
            string Meeting = LgUser.Meetting;
            MessasgeData mgdata = Datafun.MgfunctionData(" select top 10 * from  (  select a.id, a.Tel,b.UserName,a.Comment from tb_Comment a  left join tb_logingame b on a.Tel=b.Tel where a.Meeting=@Meeting ) A order by id  desc", new SqlParameter("@Meeting", Meeting));
            ViewBag.table = mgdata.Mgdata;
            return View();
        }
        /// <summary>
        /// 评论列表测试
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentResultT()
        {
            string Meeting = LgUser.Meetting;
            MessasgeData mgdata = Datafun.MgfunctionData(" select top 10 * from  (  select a.id, a.Tel,b.UserName,a.Comment from tb_Comment a  left join tb_logingame b on a.Tel=b.Tel where a.Meeting=@Meeting ) A order by id  desc", new SqlParameter("@Meeting", Meeting));
            ViewBag.table = mgdata.Mgdata;
            return View();
        }
        /// <summary>
        /// 点击列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ClickResult()
        {
            return View();
        }
        #endregion
        #region 动作
        /// <summary>
        /// id 为奖项 ids 为中奖人数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string ChoujiangAjax()
        {     

            string id=Request["va1"];
            string ids = Request["va2"];
            string bsid = id;

            StringBuilder sb = new StringBuilder();
            string scb = "";
            string sql = "";
            string sli = "";

            sql = @"select top " + ids + " * from tb_InsertMeeting where jx=0 and Meeting=" + LgUser.Meetting + " order by NEWID()";
          
            sli = @" 
                    <div class='lottery-vip-list'>
          <label>{$UserName$}</label>
       <input  type='hidden' class='hide' value='{$id$}'>
          <span>{$tel3$}******{$tel4$}</span>
        </div>            
                         ";

            MessasgeData mgdata = Datafun.MgfunctionData(sql, new SqlParameter("@id", bsid));

            foreach (DataRow dr in mgdata.Mgdata.Rows)
            {
                string tel3 =dr["Tel1"].ToString().Substring(0,3);
                string tel4 = dr["Tel1"].ToString().Substring(7,4);
                scb = sli.Replace("{$UserName$}", dr["UserName"].ToString()).Replace("{$tel3$}", tel3).Replace("{$tel4$}", tel4).Replace("{$id$}", dr["id"].ToString());
                sb.Append(scb);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 保存抽奖的数据
        /// </summary>
        /// <returns></returns>
        public string AddChoujiangAjax()
        {

            string ids = Request["va1"];
            string jx = Request["va2"];
            string[] id = ids.Split(',');
            string IsSuccess = "0";
            foreach (string mt in id)
            {
                MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"update tb_InsertMeeting set jx="+jx+" where id="+mt+"");

            }        
            return IsSuccess;

        }
        /// <summary>
        /// 进入游戏界面
        /// </summary>
        /// <returns></returns>
        public string EnterGameAjax()
        {

           
            string tel = Request["va2"];

            ViewBag.meeting = "";
            ViewBag.id = "";
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@tel",tel),                              
                                                                       
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("select MeetingName,b.id from tb_login a  left join tb_Meeting b on a.Meeting=b.id where a.GamePwd=@tel",pms);


            if (mgdata.Mgdatacount > 0)
            {
                ViewBag.meeting = mgdata.Mgdata.Rows[0]["MeetingName"];
                IsSuccess = mgdata.Mgdata.Rows[0]["id"].ToString();
                


            }
            else
            {

                IsSuccess = "0";
            }
            return IsSuccess;

        }
        /// <summary>
        /// 填写用户信息
        /// </summary>
        /// <returns></returns>
        public string UserInforGameAjax()
        {


            string ids = Request["va1"];
            string tel = Request["va2"];
            string name = Request["va3"];
          
            string IsSuccess = "1";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",ids),
                                   new SqlParameter("@UserName",name),
                                     new SqlParameter("@Tel",tel)
                                                                       
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@" if not exists(select * from tb_logingame where Meeting=@Meeting and Tel=@Tel ) begin  insert into tb_logingame(UserName,Tel,Meeting)values(@UserName,@Tel,@Meeting) end ", pms);
           
                LoginGameUser lguser = new LoginGameUser
                {
                    UserName = name,
                    Meeting = ids,
                    Tel = tel
                };
                Session["LgGameuser"] = lguser;
                IsSuccess = "0";  
            return IsSuccess;

        }
        /// <summary>
        /// 电话获取名称
        /// </summary>
        /// <returns></returns>
        public string TelGameAjax()
        {


            string ids = Request["va1"];
            string tel = Request["va2"];       
            string IsSuccess = "";
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Meeting",ids),
                                     new SqlParameter("@Tel",tel)
                                                                       
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData("select UserName from tb_InsertMeeting where Meeting=@Meeting and (Tel1=@Tel or Tel2=@Tel) ", pms);
            if (mgdata.Mgdatacount > 0)
            {


                IsSuccess = mgdata.Mgdata.Rows[0][0].ToString();

            }


            return IsSuccess;

        }
        /// <summary>
        /// 填写评论
        /// </summary>
        /// <returns></returns>
        public string CommentAjax()
        {
            string IsSuccess = "1";
            if (LgGameUser == null)
            {
                return IsSuccess = "1";
            }
            string Comment = Request["va1"];
       
            SqlParameter[] pms = {                          
                                 new SqlParameter("@Comment",Comment),
                                  new SqlParameter("@Meeting",LgGameUser.Meeting),
                                   new SqlParameter("@Tel",LgGameUser.Tel)
                                                                       
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor("insert into tb_Comment(Comment,Meeting,Tel)values(@Comment,@Meeting,@Tel)", pms);
            if (mginfor.Mgdatacount > 0)
            {
             
             
                IsSuccess = "0";

            }


            return IsSuccess;

        }
        /// <summary>
        /// 保存点击数据
        /// </summary>
        /// <returns></returns>
        public string ClicksAjax()
        {
            string IsSuccess = "1";
            if (LgGameUser == null)
            {
                return IsSuccess = "1";
            }
            string nums = Request["va1"];

            SqlParameter[] pms = {                          
                                 new SqlParameter("@nums",nums),
                                  new SqlParameter("@Meeting",LgGameUser.Meeting),
                                   new SqlParameter("@Tel",LgGameUser.Tel),
                                    new SqlParameter("@UserName",LgGameUser.UserName)
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor("insert into tb_clicks(Nums,Meeting,Tel,LoginName)values(@nums,@Meeting,@Tel,@UserName)", pms);
            if (mginfor.Mgdatacount > 0)
            {


                IsSuccess = "0";

            }


            return IsSuccess;

        }
        /// <summary>
        /// 启动游戏
        /// </summary>
        /// <returns></returns>
        public string StartClick()
        {

            string Meeting = LgUser.Meetting;
            string IsSuccess = "0";
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"update tb_GameSet set status=1 where Meeting=@Meeting;Update tb_clicks set isdel=1 where Meeting=@Meeting", new SqlParameter("@Meeting", Meeting));
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
        /// 关闭游戏
        /// </summary>
        /// <returns></returns>
        public string EndClick()
        {

            string Meeting = LgUser.Meetting;
            string IsSuccess = "0";
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(@"update tb_GameSet set status=0 where Meeting=@Meeting", new SqlParameter("@Meeting", Meeting));
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
        #endregion
    }
}
