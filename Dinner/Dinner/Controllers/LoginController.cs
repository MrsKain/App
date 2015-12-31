using Dinner.Models;
using Kain_class.Messageinfor;
using Kain_class.Pass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dinner.Controllers
{
    public class LoginController : MainsController
    {
        //
        // GET: /Login/
        #region 视图
        public ActionResult Login()
        {
            return View();
        }
        #endregion
        #region 动作
        [HttpPost]
        public string LoginActionUser()
        {
            string username = Request["va1"];
            string password = Request["va2"];
            string IsSuccess = "0";
            string time = DateTime.Now.ToString();
            string sql = @"select * from tb_login where UserName=@UserName and Password=@Password and isdel=0 and  convert(nvarchar(11),Start_Date,120)<= convert(nvarchar(11),getdate(),120) and  convert(nvarchar(11),End_Date,120)>= convert(nvarchar(11),getdate(),120)";
            SqlParameter[] pms = { 
                                        new SqlParameter("@UserName",username), 
                                       
                                         new SqlParameter("@Password",Passwd.SetPass(password))
                                        
                                        };
            MessasgeData mgdata = Datafun.MgfunctionData(sql, pms);

            if (mgdata.Mgdatacount > 0)
            {
                DataRow dr = mgdata.Mgdata.Rows[0];
                LoginUser lguser = new LoginUser
                {
                    UserId = dr["id"].ToString(),
                    UserName = dr["UserName"].ToString(),
                    UserPwd = dr["Password"].ToString(),
                    UserLvl = int.Parse(dr["Lvl"].ToString()),
                    Bank = dr["Bank"].ToString(),
                    Tel = dr["Tel"].ToString(),
                    Meetting = dr["Meeting"].ToString()
                };
                Session["Lguser"] = lguser;


                Response.Cookies["loginname"].Value = Passwd.SetPass(username);
                Response.Cookies["loginpwd"].Value = password;
                Response.Cookies["loginname"].Expires = DateTime.Now.AddDays(10);
                Response.Cookies["loginpwd"].Expires = DateTime.Now.AddDays(10);

                mgdata.Mgcontent = "登陆成功";
                if (lguser.UserLvl == 0)
                {
                    IsSuccess = "1";

                }
                else
                {
                    IsSuccess = "2";
                }

            }
            else
            {
                mgdata.Mgcontent = "用户名密码错误";
                IsSuccess = "0";
            }
            return IsSuccess;
        }
        #endregion

    }
}
