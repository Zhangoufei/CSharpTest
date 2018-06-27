using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Admin;
using BonSite.Web.Admin.Models;
using System.Data;

namespace BonSite.Web.Admin.Controllers
{
    public class ReviewController : BaseAdminController
    {
        JsonResult json = new JsonResult();
        public ActionResult List(string articleTitle, string Keywords, string sortColumn, string sortDirection, string dayTime, string status, string setArticleClassId, int? articleClassId1, int pageSize = 10, int pageNumber = 1)
        {
            if (setArticleClassId == null)
            {
                setArticleClassId = "";
            }
            //查询条件
            string condition = " [DisplayType] in (0,2,4) ";
            //审核状态 选中状态
            string[] statusArray = new string[3];
            if (!string.IsNullOrEmpty(status) && !status.Equals("4"))
            {
                statusArray[Convert.ToInt32(status)] = "selected";
                condition = condition + " and ApprovalStatus=" + status;
            }
            ViewData["statusArray"] = statusArray;
            if (!string.IsNullOrEmpty(setArticleClassId)&&Convert.ToInt32(setArticleClassId) > 0)
            {
                DataTable articleClassDt = Article.GetArtClaIdByParentArticleClassIDs(Convert.ToInt32(setArticleClassId));
                if (articleClassDt!=null && articleClassDt.Rows.Count > 0)
                {
                    string articleClasss = "";                    
                    for (int i = 0; i < articleClassDt.Rows.Count; i++)
                    {
                        DataTable articleClassDt2 = Article.GetArtClaIdByParentArticleClassIDs(Convert.ToInt32(articleClassDt.Rows[i]["ArticleClassID"].ToString()));
                        if (articleClassDt2 != null && articleClassDt2.Rows.Count > 0)
                        {
                            for (int j = 0; j < articleClassDt2.Rows.Count; j++)
                            {
                                articleClasss += Convert.ToInt32(articleClassDt2.Rows[j]["ArticleClassID"].ToString()) + ",";
                            }
                        }
                        else
                        {
                            articleClasss+=Convert.ToInt32(articleClassDt.Rows[i]["ArticleClassID"].ToString()) + ",";
                        }
                        
                    }
                    articleClasss = articleClasss.Substring(0, articleClasss.Length - 1);
                    condition = condition + "  and ArticleClassID in " + "("+articleClasss+")";
                }
                else
                {
                    //没有下级菜单
                    condition = condition + " and ArticleClassID=" + Convert.ToInt32(setArticleClassId);
                }
            }
            ViewData["setArticleClassId"] = setArticleClassId;
            MenuModel menuModel = new MenuModel();
            menuModel.MenuList = BonSite.Services.ArticleClass.AdminGetArticleClassTreeList();
            ViewData["MenuList"] = menuModel.MenuList;
            //时间选择状态
            if (!string.IsNullOrEmpty(dayTime))
            {
                DateTime dtStart = TypeHelper.StringToDateTime(dayTime);
                DateTime dtEnd = dtStart.AddDays(1);
                condition = condition + " and AddTime > '" + dtStart + "' and AddTime < '" + dtEnd + "'";
            }
            ViewData["dayTime"] = dayTime;
            //标题关键字
            if (Keywords != null && !string.IsNullOrEmpty(Keywords))
            {
                ViewData["Keywords"] = Keywords;
                condition = condition + " and Title like '%" + Keywords + "%'";
            }
            


            pageSize = WorkContext.SiteConfig.pageSize;
            int articleClassId = 0;

            if (articleClassId1 == null)
            {
                articleClassId = 299;
            }
            else
            {
                articleClassId = (int)articleClassId1;
            }

            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);
            //string condition = Article.AdminGetArticleListCondition(articleClassId, articleTitle);
            //string condition1 = condition + @" or [Keyword] = 1  ";
            string sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition));
            //
            ArticleListModel model = new ArticleListModel()
            {
                DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                ArticleClassID = articleClassId,
                ArticleTitle = articleTitle,
                ClassInfo = classInfo
            };
            //foreach (DataRow item in model.DataList.Rows)
            //{
            //    //获取一级分类
            //    int ParentArticleClassID = 0;
            //    int ArticleClassID = Convert.ToInt32(item["ArticleClassID"].ToString());
            //    ParentArticleClassID = Article.GetNameByArticleClassID2(ArticleClassID);
            //    if (ParentArticleClassID > 0)
            //    {
            //        ArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
            //        if (ArticleClassID > 0)
            //        {
            //            ParentArticleClassID = Article.GetNameByArticleClassID2(ArticleClassID);
            //            if (ParentArticleClassID > 0)
            //            {
            //                ArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
            //                if (ArticleClassID > 0)
            //                {
            //                    ParentArticleClassID = Article.GetNameByArticleClassID2(ArticleClassID);
            //                    if (ParentArticleClassID > 0)
            //                    {

            //                    }
            //                    else
            //                    {
            //                        //如果没有上级菜单，则取当前栏目
            //                        ParentArticleClassID = ArticleClassID;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //如果没有上级菜单，则取当前栏目
            //                ParentArticleClassID = ArticleClassID;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        //如果没有上级菜单，则取当前栏目
            //        ParentArticleClassID = ArticleClassID;
            //    }
            //    item["ArticleClassID"] = ParentArticleClassID;
            //}
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;
            SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&articleClassId={5}&newsTitle={6}",
                                                          Url.Action("List"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection,
                                                          articleClassId,
                                                          articleTitle));
            return View(model);
        }
        //单个修改
        public ActionResult statusEdit(int articleid, int approvalStatus)
        {
            if (editApprovalStatus(articleid, approvalStatus))
            {
                return PromptView("审核操作成功");
            }
            else
            {
                return PromptView("审核操作失败");
            }
        }
        //批量修改
        [HttpPost]
        public JsonResult batchStatusEdits(string articleidArry, string approvalStatus, string status)
        {
            //string approvalStatus = HttpContext.Request.Form["articleid"];
            //string articleidArry = HttpContext.Request.Form["articleid"];
            string[] articleid = articleidArry.Split(',');
            //总行数
            int allCount = articleid.Length - 1;
            //成功
            int successCount = 0;
            //失败
            int errorCount = 0;
            for (int i = 0; i < allCount; i++)
            {
                if (editApprovalStatus(Convert.ToInt32(articleid[i]), Convert.ToInt32(approvalStatus)))
                {
                    successCount++;
                }
            }
            errorCount = allCount - successCount;
            json.Data = new { msg = "修改成功数：" + successCount + "修改失败数：" + errorCount, status = status };
            return json;
        }
        //操作方法
        public bool editApprovalStatus(int articleid, int approvalStatus)
        {
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);
            if (articleInfo == null)
                return false;
            articleInfo.ApprovalStatus = approvalStatus;
            articleInfo.Auditor = ck;
            if (Article.Update(articleInfo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public JsonResult JsonStatusSearch()
        {
            string status = HttpContext.Request.Form["status"];
            json.Data = new { status = status };
            return json;
        }
        //预览
        public ActionResult Preview(int id = -1)
        {
            ArticleInfo articleInfo = Article.GetModelByArticleID(id);
            ArticleClassInfo articleClassInfo = ArticleClass.GetModelById(articleInfo.ArticleClassID);
            ArticleModel model = new ArticleModel()
           {
               ArticleClassID = articleInfo.ArticleClassID,
               Title = articleInfo.Title,
               SpecialID = articleInfo.SpecialID,
               DisplayType = articleInfo.DisplayType,
               Url = articleInfo.Url,
               Digest = articleInfo.Digest,
               ImgUrl = articleInfo.ImgUrl,
               Body = articleInfo.Body,
               Author = articleInfo.Author,
               ComeForm = articleInfo.ComeForm,
               IsShow = articleInfo.IsShow,
               IsHome = articleInfo.IsHome,
               IsBest = articleInfo.IsBest,
               IsTop = articleInfo.IsTop,
               AddTime = articleInfo.AddTime,
               Keyword = articleInfo.Keyword,
               Description = articleInfo.Description,
               InformType = articleInfo.InformType,
               EndTime = articleInfo.EndTime,
               InformGroup = articleInfo.InformGroup,
               MicroVideo = articleInfo.MicroVideo

           };
            if (articleInfo != null)
            {
                if (articleInfo.DisplayType.Equals(4))
                {
                    ViewData["actlcle"] = "display:none";
                    ViewData["video"] = "";
                    ViewData["ArticleImg"] = "display:none";
                }
                else
                {
                    ViewData["actlcle"] = "";
                    ViewData["video"] = "display:none";
                    ViewData["ArticleImg"] = "display:none";
                }
                if (articleClassInfo.ClassType.Equals(3))
                {
                    ViewData["actlcle"] = "";
                    ViewData["video"] = "display:none";
                    ViewData["ArticleImg"] = "";
                }
                //外部链接转向
                if (articleInfo.DisplayType.Equals(1))
                    return Redirect(articleInfo.Url);

            }
            return View(model);
        }
        //批量删除
        [HttpPost]
        public JsonResult batchDel(string articleidArry, string status)
        {
            //string approvalStatus = HttpContext.Request.Form["articleid"];
            //string articleidArry = HttpContext.Request.Form["articleid"];
            string[] articleid = articleidArry.Split(',');
            int count = articleid.Length - 1;
            int[] intTemp = new int[count];
            for (int i = 0; i < count; i++)
            {
                intTemp[i] = int.Parse(articleid[i]);
            }
            Article.Del(intTemp);
            AddAdminOperateLog("删除内容", "删除内容,内容ID为:" + CommonHelper.IntArrayToString(intTemp));
            json.Data = new { msg = "删除成功数：" + count, status = status };
            return json;
        }
    }
}




