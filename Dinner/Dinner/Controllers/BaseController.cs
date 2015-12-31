using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dinner.Models;
using Kain_class.Messageinfor;
using Kain_class.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;

namespace Dinner.Controllers
{
    public class BaseController :MainsController
    {
        //
        // GET: /Base/

     

       
        /// <summary>
        /// 查找会议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMeeting()
        {
            string ReturnValue = string.Empty;
            MessasgeData mgdata = Datafun.MgfunctionData(" select id,MeetingName from tb_Meeting where isdel=0 ");

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }
        /// <summary>
        /// 查找会议
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetMeetingFen()
        {
            string ReturnValue = string.Empty;
            string  sql="";
            if (LgUser.Meetting=="")
            {
            sql="select id,MeetingName from tb_Meeting where isdel=0"; 
            }
            else
            {
             sql = "select id,MeetingName from tb_Meeting where isdel=0 and id=@id"; 
            }
            MessasgeData mgdata = Datafun.MgfunctionData(sql,new SqlParameter("@id",LgUser.Meetting));

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }

        /// <summary>
        /// 查找备注（根据导入的数据查找备注）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetBank()
        {
            string ReturnValue = string.Empty;
            MessasgeData mgdata = Datafun.MgfunctionData("select distinct Bank from tb_InsertMeeting where  Meeting=@Meeting and Bank <>''", new SqlParameter("@Meeting",LgUser.Meetting));

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }
        /// <summary>
        /// 密保问题数据绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuestion()
        {
            string ReturnValue = string.Empty;
            MessasgeData mgdata = Datafun.MgfunctionData("select id,Question from dbo.tb_question where isdel=0");

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }
        /// <summary>
        /// 省份绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProvince()
        {
            string ReturnValue = string.Empty;
            MessasgeData mgdata = Datafun.MgfunctionData("select ccode,cname from dbo.bm_region where arealevel=1");

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }
        /// <summary>
        /// 省份绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCity(string id)
        {
            string ReturnValue = string.Empty;
            MessasgeData mgdata = Datafun.MgfunctionData("select ccode,cname from dbo.bm_region where pcode=@id",new SqlParameter("@id",id));

            ReturnValue = Jsonzs.DataTableToJson(mgdata.Mgdata, '"');
            return ContentJson(ReturnValue);

        }
        /// <summary>
        /// Menus 菜单
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult MenuPart()
        {
          

            ViewBag.menu =menus(LgUser.UserLvl);
          
            return PartialView();
        }
        /// <summary>
        /// head菜单
        /// </summary>
        /// <param name="lc"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult HeadPart()
        {


            ViewBag.bank = LgUser.Bank;

            return PartialView();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult FenYe(int pagesize, int recordCount, int pageIndex, int pageitem, string pageUrl, string strWhere)
        {

            string sc = Pagesz(recordCount,  pageitem, pageUrl, strWhere,pageIndex,pagesize);
            ViewBag.page = sc;
            return PartialView();
        
        
        }
        /// <summary>
        /// 底部分区负责人
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Foot(string id)
        {
            ViewBag.id = id;
            ViewBag.lvl = LgUser.UserLvl;
            return PartialView();
        }
      
        /// <summary>
        /// 启用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string StartUsing()
        {
            string id = Request["va1"];
            string table = Request["va2"];
            string IsSuccess = "0";

            string sql = @"Update " + table + " set Status=0 where id=@id";
            SqlParameter[] pms = { 
                                        new SqlParameter("@id",id),
                                 
                                        
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(sql, pms);

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
        /// 禁用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Forbidden()
        {
            string id = Request["va1"];
            string table = Request["va2"];
            string IsSuccess = "0";

            string sql = @"Update " + table + " set Status=1 where id=@id";
            SqlParameter[] pms = { 
                                        new SqlParameter("@id",id)
                               
                                        
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(sql, pms);

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
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Deletes()
        {
            string id = Request["va1"];
            string table = Request["va2"];
            string IsSuccess = "0";

            string sql = @"Update " + table + " set isdel=1 where id=@id";
            SqlParameter[] pms = { 
                                        new SqlParameter("@id",id)
                               
                                        
                                        };
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(sql, pms);

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
        /// 真删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Deletesure()
        {
          
            string table = Request["va2"];
            string IsSuccess = "0";

            string sql = @"update   " + table + "  set isdel=3 where isdel=1 ";
        
            MessasgeInfor mginfor = Datafun.Mgfunctioninfor(sql);

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
        /// 编辑器图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadEdit()
        {
            MessasgeInfor mginfor = new MessasgeInfor { Mgbool = false, Mgcontent = "", Mgtitle = "系统提示", Mgdatacount = 0 };
            Hashtable hash = new Hashtable();
            mginfor = UploadImg("imgFile", "UploadFiles/Uimages", 400, 100, 100, 800, 900);
            string js = string.Format("{0}\"error\":{1},\"url\":\"{2}\",\"message\":\"{3}\"{4}", "{", mginfor.Mgbool ? 0 : 1,
                mginfor.Mgcontent.Split('$')[0], mginfor.Mgbool ? "上传成功！" : "上传失败(请不要上传超过400kb的图片)", "}");
            return ContentJson(js);
        }

        /// <summary>
        /// 上传图片多参数
        /// </summary>
        /// <param name="fileskey"></param>
        /// <param name="uploadpath"></param>
        /// <param name="filesize"></param>
        /// <param name="minWidth"></param>
        /// <param name="minHeight"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private MessasgeInfor UploadImg(string fileskey, string uploadpath, int filesize, int minWidth, int minHeight, int maxWidth, int maxHeight)
        {
            MessasgeInfor mgInfor = new MessasgeInfor { Mgbool = false, Mgcontent = "", Mgtitle = "系统提示", Mgdatacount = 0 };
            System.IO.Stream stream = null;
            System.Drawing.Image originalImg = null;   //原图
            System.Drawing.Image thumbImg = null;      //缩放图       
            try
            {
                string resultTip = string.Empty;  //返回信息

                HttpPostedFileBase file = Request.Files[fileskey];      //上传文件      

                string uploadPath = Server.MapPath("~/Content/" + uploadpath);  //得到上传路径

                string lastImgUrl = @Request.Params["LastImgUrl"];

                if (!string.IsNullOrEmpty(lastImgUrl))
                {
                    FileDel(Server.MapPath(lastImgUrl));
                }
                if (file != null)
                {
                    if (!System.IO.Directory.Exists(uploadPath))
                    {
                        System.IO.Directory.CreateDirectory(uploadPath);
                        //mgInfor.Mgbool = false;
                        //mgInfor.Mgcontent = "不存在";
                        //return mgInfor;
                    }
                    if (file.ContentLength > filesize * 1024)
                    {
                        FileDel(Server.MapPath(lastImgUrl));
                        mgInfor.Mgbool = false;
                        mgInfor.Mgcontent = string.Format("文件大小超过了{0}kb", filesize);
                        return mgInfor;
                    }

                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();   //上传文件的后缀（小写）


                    if (ext == ".jpg" || ext == ".png")
                    {
                        string flag = "edit_" + DateTime.Now.ToFileTime() + ext;
                        string uploadFilePath = uploadPath + "\\" + flag;   //缩放图文件路径
                        stream = file.InputStream;
                        originalImg = System.Drawing.Image.FromStream(stream);
                        if (originalImg.Width >= minWidth && originalImg.Height >= minHeight)
                        {
                            //  thumbImg = originalImg;/
                            thumbImg = GetThumbNailImage(originalImg, maxWidth, maxHeight);  //按宽、高缩放
                            if (thumbImg.Width >= minWidth && thumbImg.Height >= minHeight)
                            {
                                thumbImg.Save(uploadFilePath);
                                mgInfor.Mgcontent = "/Content/" + uploadpath + "/" + flag + "$" + thumbImg.Width + "$" + thumbImg.Height;
                                //mgInfor.Mgcontent = "{ url:\"\\Content\\imgTemp" + "\\" + flag + "\",w:" + thumbImg.Width + " }";
                                mgInfor.Mgbool = true;
                            }
                            else
                            {
                                mgInfor.Mgcontent = "图片比例不符合要求";
                            }
                        }
                        else
                        {
                            mgInfor.Mgcontent = "图片尺寸必须大于" + minWidth + "*" + minHeight;
                            FileDel(Server.MapPath(lastImgUrl));
                        }
                    }
                }
                else
                {
                    mgInfor.Mgbool = false;

                    mgInfor.Mgcontent = "上传文件为空";
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (originalImg != null)
                {
                    originalImg.Dispose();
                }

                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }

                if (thumbImg != null)
                {
                    thumbImg.Dispose();
                }

                GC.Collect();
            }
            return mgInfor;
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path"></param>
        public static void FileDel(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
        ///<summary>
        /// 对给定的一个图片（Image对象）生成一个指定大小的缩略图。
        ///</summary>
        ///<param name="originalImage">原始图片</param>
        ///<param name="thumMaxWidth">缩略图的宽度</param>
        ///<param name="thumMaxHeight">缩略图的高度</param>
        ///<returns>返回缩略图的Image对象</returns>
        public static System.Drawing.Image GetThumbNailImage(System.Drawing.Image originalImage, int thumMaxWidth, int thumMaxHeight)
        {
            System.Drawing.Size thumRealSize = System.Drawing.Size.Empty;
            System.Drawing.Image newImage = originalImage;
            System.Drawing.Graphics graphics = null;
            try
            {
                thumRealSize = GetNewSize(thumMaxWidth, thumMaxHeight, originalImage.Width, originalImage.Height);
                newImage = new System.Drawing.Bitmap(thumRealSize.Width, thumRealSize.Height);
                graphics = System.Drawing.Graphics.FromImage(newImage);
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.Clear(System.Drawing.Color.Transparent);
                graphics.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, thumRealSize.Width, thumRealSize.Height), new System.Drawing.Rectangle(0, 0, originalImage.Width, originalImage.Height), System.Drawing.GraphicsUnit.Pixel);
            }
            catch { }
            finally
            {
                if (graphics != null)
                {
                    graphics.Dispose();
                    graphics = null;
                }
            }
            return newImage;
        }
        ///<summary>
        /// 获取一个图片按等比例缩小后的大小。
        ///</summary>
        ///<param name="maxWidth">需要缩小到的宽度</param>
        ///<param name="maxHeight">需要缩小到的高度</param>
        ///<param name="imageOriginalWidth">图片的原始宽度</param>
        ///<param name="imageOriginalHeight">图片的原始高度</param>
        ///<returns>返回图片按等比例缩小后的实际大小</returns>
        public static System.Drawing.Size GetNewSize(int maxWidth, int maxHeight, int imageOriginalWidth, int imageOriginalHeight)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(imageOriginalWidth);
            double sh = Convert.ToDouble(imageOriginalHeight);
            double mw = Convert.ToDouble(maxWidth);
            double mh = Convert.ToDouble(maxHeight);
            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = maxWidth;
                h = (w * sh) / sw;
            }
            else
            {
                h = maxHeight;
                w = (h * sw) / sh;
            }
            return new System.Drawing.Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
        #region 枚举
        /// <summary>
        /// 编号枚举
        /// </summary>
        public enum BHenum
        {
            /// <summary>
            /// 商品编号
            /// </summary>
            Goods_BH = 0,
            /// <summary>
            /// 订单编号
            /// </summary>
            Order_BH = 1,
            /// <summary>
            /// 购物车编号
            /// </summary>
            Buyshop_BH = 2

        }
        /// <summary>
        /// 编号计算
        /// </summary>
        /// <param name="bh"></param>
        /// <returns></returns>
        public string BhCreateAction(BHenum bh)
        {
            string sd = "GS";
            switch (bh)
            {
                case BHenum.Goods_BH:
                    sd = "GS"; break;
                case BHenum.Order_BH:
                    sd = "OE"; break;
                case BHenum.Buyshop_BH:
                    sd = "BS"; break;
                default: break;
            }
            return sd + DateTime.Now.ToString("yyyyMMddHHmmssfff");

        }
        #endregion

        #region menus
        public string menus(int lvl)
        {

            DataTable dt = tablehtml();
            StringBuilder shtml = new StringBuilder();
            StringBuilder sb = new StringBuilder();
           string sli="";
           if (lvl ==0)
           {
               #region
               sli = @"<ul class='nav sidebar-menu'>

                    <li class='active'>
                        <a href='/Sellers/index'>
                            <i class='menu-icon glyphicon glyphicon-home'></i>
                            <span class='menu-text'>溯源管理系统</span>
                        </a>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-th'></i>
                            <span class='menu-text'>标识管理</span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                           <li>
                                <a href='/other/DataInsert'>
                                    <span class='menu-text'>数据源导入</span>
                                </a>
                            </li>                        
                             <li>
                                <a href='/BS/MemberAddBS'>
                                    <span class='menu-text'>标识有序发放</span>
                                </a>
                            </li>
                             <li>
                                <a href='/BS/MemberAddBSwx'>
                                    <span class='menu-text'>标识无序发放</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Bs/BSSearch'>
                                    <span class='menu-text'>标识追踪</span>
                                </a>
                            </li>
                            <li>
                                <a href='/BS/BSIndex'>
                                    <span class='menu-text'>标识发放管理</span>
                                </a>
                            </li>
                            <li>
                                <a href='/BS/BSSearchIndex'>
                                    <span class='menu-text'>标识查询记录</span>
                                </a>
                            </li>

                        </ul>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-desktop'></i>
                            <span class='menu-text'>产品管理 </span>
                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/Product/ProductList'>
                                    <span class='menu-text'>产品管理</span>
                                </a>
                            </li>


                            <li>
                                <a href='/Product/ProductTypeList'>
                                    <span class='menu-text'>产品分类</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Product/ProductHistroyList'>
                                    <span class='menu-text'>成长日志</span>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-table'></i>
                            <span class='menu-text'>其他管理 </span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/other/BSlink'>
                                    <span class='menu-text'>二维码查询链接</span>
                                </a>
                            </li>
                            <li>
                                <a href='/other/procedurelist'>
                                    <span class='menu-text'>流程工序设置</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Other/UserManagerList'>
                                    <span class='menu-text'>用户管理</span>
                                </a>
                            </li>
                           
                   
                        </ul>
                    </li>
                       <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-magic'></i>
                            <span class='menu-text'>企业管理 </span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/Company/Companylist'>
                                    <span class='menu-text'>企业用户</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Company/CompanyTypeList'>
                                    <span class='menu-text'>企业分类</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>";
               #endregion
           }

           else if (lvl == 1)
           {
               #region
               sli = @"<ul class='nav sidebar-menu'>

                    <li class='active'>
                        <a href='/Sellers/index'>
                            <i class='menu-icon glyphicon glyphicon-home'></i>
                            <span class='menu-text'>溯源管理系统</span>
                        </a>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-th'></i>
                            <span class='menu-text'>标识管理</span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                           <li>
                                <a href='/other/DataInsert'>
                                    <span class='menu-text'>数据源导入</span>
                                </a>
                            </li>                       
                             <li>
                                <a href='/BS/MemberAddBS'>
                                    <span class='menu-text'>标识有序发放</span>
                                </a>
                            </li>
                             <li>
                                <a href='/BS/MemberAddBSwx'>
                                    <span class='menu-text'>标识无序发放</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Bs/BsSearch'>
                                    <span class='menu-text'>标识追踪</span>
                                </a>
                            </li>
                            <li>
                                <a href='/BS/BSIndex'>
                                    <span class='menu-text'>标识发放管理</span>
                                </a>
                            </li>
                            <li>
                                <a href='/BS/BSSearchIndex'>
                                    <span class='menu-text'>标识查询记录</span>
                                </a>
                            </li>

                        </ul>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-desktop'></i>
                            <span class='menu-text'>产品管理 </span>
                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/Product/ProductList'>
                                    <span class='menu-text'>产品管理</span>
                                </a>
                            </li>


                            <li>
                                <a href='/Product/ProductTypeList'>
                                    <span class='menu-text'>产品分类</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Product/ProductHistroyList'>
                                    <span class='menu-text'>成长日志</span>
                                </a>
                            </li>
                        </ul>
                    </li>

                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-table'></i>
                            <span class='menu-text'>其他管理 </span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/other/BSlink'>
                                    <span class='menu-text'>二维码查询链接</span>
                                </a>
                            </li>
                            <li>
                                <a href='/other/procedurelist'>
                                    <span class='menu-text'>流程工序设置</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Other/UserManagerList'>
                                    <span class='menu-text'>用户管理</span>
                                </a>
                            </li>
                           
                   
                        </ul>
                    </li>
                       <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-magic'></i>
                            <span class='menu-text'>企业管理 </span>

                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                            <li>
                                <a href='/Company/Companylist'>
                                    <span class='menu-text'>企业用户</span>
                                </a>
                            </li>
                            <li>
                                <a href='/Company/CompanyTypeList'>
                                    <span class='menu-text'>企业分类</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                </ul>";
               #endregion
           }
           else
           {
               #region
               sli = @"<ul class='nav sidebar-menu'>

                    <li class='active'>
                        <a href='/Sellers/index'>
                            <i class='menu-icon glyphicon glyphicon-home'></i>
                            <span class='menu-text'>溯源管理系统</span>
                        </a>
                    </li>
                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-th'></i>
                            <span class='menu-text'>标识管理</span>

                            <i class='menu-expand'></i>
                        </a>                    
                    </li>
                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-desktop'></i>
                            <span class='menu-text'>产品管理 </span>
                            <i class='menu-expand'></i>
                        </a>

                        <ul class='submenu'>
                                                   
                            <li>
                                <a href='/Product/ProductHistroyList'>
                                    <span class='menu-text'>成长日志</span>
                                </a>
                            </li>
                        </ul>
                    </li>
                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-table'></i>
                            <span class='menu-text'>其他管理 </span>

                            <i class='menu-expand'></i>
                        </a>                     
                    </li>
                    <li>
                        <a href='#' class='menu-dropdown'>
                            <i class='menu-icon fa fa-magic'></i>
                            <span class='menu-text'>企业管理 </span>

                            <i class='menu-expand'></i>
                        </a>
                    </li>
                </ul>";
               #endregion

           }
           

            sb.Append(sli);
            return sb.ToString();
        }
        public DataTable tablehtml(string ParentsId = "0")
        {
            LoginUser loginuser = Session["sysadmin"] as LoginUser;
            MessasgeData mgdata = Datafun.MgfunctionData("select * from tb_menu where ParentsId=@ParentsId and isdel=0 and LOCATE('" + LgUser.UserLvl + "',RoleLvl)>0 ", new SqlParameter("@ParentsId", ParentsId));
            return mgdata.Mgdata;
        }
        #endregion
    }
}
