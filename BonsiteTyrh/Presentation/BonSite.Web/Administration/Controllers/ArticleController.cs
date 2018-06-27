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
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Drawing;
using Newtonsoft.Json;
using System.Data;
using BonSite.Core.Domain.Log;

namespace BonSite.Web.Admin.Controllers
{
    public class ArticleController : BaseAdminController
    {
        //
        // GET: /Article/

        //public ActionResult Index()
        //{
        //    return View();
        //}

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="articleTitle"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <param name="articleClassId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public ActionResult List(string articleTitle, string sortColumn, string sortDirection, int articleClassId, int pageSize = 10, int pageNumber = 1)
        {
            pageSize = WorkContext.SiteConfig.pageSize;
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);

            if (classInfo == null)
                return PromptView("文章分类不存在");

            ////管理菜单
            //if ((classInfo.ClassType.Equals(-1))||(classInfo.ClassType.Equals(0)))
            //    Response.Redirect(classInfo.AdminUrl);

            //管理菜单
            if ((classInfo.ClassType.Equals(-1)) || (classInfo.ClassType.Equals(0)))
            {
                Response.Redirect(classInfo.AdminUrl + @"?articleClassId1=" + articleClassId);
            }
            //单页
            if (classInfo.ClassType.Equals(1))
                return RedirectToAction("Page", new { articleClassId = articleClassId });

            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classInfo.ArticleClassID);
            string condition = "";
            string condition1 = "";
            string sort = "";
            if (!partUserInfo.AdminGroupID.Equals(WorkContext.SiteConfig.wzAdmin))
            {
                condition1 = " and AdminID=" + partUserInfo.UserID;
            }
            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition));
            ArticleListModel model = new ArticleListModel();
            model.DataList = new DataTable();
            if (list.Rows.Count > 0)
            {
                int count = 0;
                int newscount = 0;
                foreach (DataRow dr in list.Rows)
                {
                    condition = Article.AdminGetArticleListCondition(Convert.ToInt32(dr["ArticleClassID"].ToString()), articleTitle) + condition1;
                    sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
                    int artnum = Article.AdminGetArticleCount(condition);
                    newscount += artnum;
                    DataTable newsList = Article.AdminGetArticleList(1000, 1, condition, sort);
                    if (count <= 0)
                    {
                        model.DataList = newsList.Clone();
                    }
                    foreach (DataRow item in newsList.Rows)
                    {
                        model.DataList.ImportRow(item);
                    }
                    count++;
                }
                DataTable newdt = model.DataList.Copy();
                newdt.Clear();
                int newdrnum = 0;
                //读取第九行出问题
                //
                //开始行，从0开始，10，20
                int rowbegin = (pageNumber - 1) * pageSize;
                //结束行，如果数据是10的整倍数
                int rowend = pageNumber * pageSize;
                //总行数
                int allcount = model.DataList.Rows.Count;
                //总页数
                float pagecount = allcount / 10;
                int allPage = (int)Math.Floor(pagecount);
                if (pageNumber > allPage)
                {
                    //说明是最后一页的倍数
                    rowend = allcount;
                }
                for (int i = rowbegin; i < rowend; i++)
                {
                    DataRow newdr = newdt.NewRow();
                    DataRow dr = model.DataList.Rows[i];
                    foreach (DataColumn column in model.DataList.Columns)
                    {
                        newdr[column.ColumnName] = dr[column.ColumnName];
                    }
                    newdt.Rows.Add(newdr);
                }
                pageModel = new PageModel(pageSize, pageNumber, model.DataList.Rows.Count);
                model.SortColumn = sortColumn;
                model.SortDirection = sortDirection;
                model.ArticleClassID = articleClassId;
                model.ArticleTitle = articleTitle;
                model.ClassInfo = classInfo;
                model.PageModel = pageModel;
                model.DataList = newdt;
                //获取上级栏目名称
                int num = 0;
                DataTable nametd = new DataTable();
                foreach (DataRow dt in newdt.Rows)
                {
                    DataTable ntd = Article.GetNameByArticleClassID(Convert.ToInt32(dt["ArticleClassID"].ToString()));
                    if (num <= 0)
                    {
                        nametd = ntd.Clone();
                    }
                    foreach (DataRow ds in ntd.Rows)
                    {
                        nametd.ImportRow(ds);
                    }
                    num++;
                    ViewData["namelist"] = nametd;
                }

            }
            else if (articleClassId == 263)
            {
                condition1 = "";
                if (partUserInfo.AdminGroupID.Equals(WorkContext.SiteConfig.wzAdmin))
                {
                    ViewData["wzAdmin"] = WorkContext.SiteConfig.wzAdmin;
                }
                else if (partUserInfo.AdminGroupID.Equals(WorkContext.SiteConfig.dzbpAdmin))
                {
                }
                else
                {
                    condition1 = " and AdminID=" + partUserInfo.UserID;
                }
                ViewData["AdminGroupID"] = partUserInfo.AdminGroupID;
                condition = " [DisplayType] in (0,2) " + " and ApprovalStatus=1 or ArticleClassID=263" + condition1;
                sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
                pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition));
                model.DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort);
                model.PageModel = pageModel;
                model.SortColumn = sortColumn;
                model.SortDirection = sortDirection;
                model.ArticleClassID = articleClassId;
                model.ArticleTitle = articleTitle;
                model.ClassInfo = classInfo;
            }
            else
            {
                if (articleClassId == 262)
                {
                    condition1 = "";
                }
                condition = Article.AdminGetArticleListCondition(articleClassId, articleTitle) + condition1;
                sort = Article.AdminGetArticleListSort(sortColumn, sortDirection);
                pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition));
                model.DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort);
                model.PageModel = pageModel;
                model.SortColumn = sortColumn;
                model.SortDirection = sortDirection;
                model.ArticleClassID = articleClassId;
                model.ArticleTitle = articleTitle;
                model.ClassInfo = classInfo;
            }
            try
            {
                SiteUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}&articleClassId={5}&newsTitle={6}",
                                                                   Url.Action("List"),
                                                                   pageModel.PageNumber,
                                                                   pageModel.PageSize,
                                                                   sortColumn,
                                                                   sortDirection,
                                                                   articleClassId,
                                                                   articleTitle));
            }
            catch (Exception)
            {

            }

            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;
            ViewData["UserID"] = partUserInfo.UserID;
            ViewData["wzglAdmin"] = WorkContext.SiteConfig.wzAdmin;
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int articleid = -1, int DisplayType = -1)
        {
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);
            if (DisplayType >= 0)
            {
                articleInfo.DisplayType = DisplayType;
            }
            //班牌专栏显示隐藏
            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleInfo.ArticleClassID);
            List<BonSite.Core.ArticleInfo.InformGroupItem> InformGroupList = new List<BonSite.Core.ArticleInfo.InformGroupItem>();
            // string url = "10.0.64.176:2013/api/noticeSys/loadGroup";



            if (BSConfig.SysConfig.EnableIpCard)
            {
                string url = BSConfig.SysConfig.IpCard + "/loadGroup";
                string ss = WebHelper.PostWebReq(url);
                if (!string.IsNullOrEmpty(ss))
                {
                    //解析json
                    JObject jo = JObject.Parse(ss);
                    string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
                    string grup = values[0];
                    string[] grups = grup.Split(',');
                    foreach (string r in grups)
                    {
                        ArticleInfo.InformGroupItem ifg = new ArticleInfo.InformGroupItem();
                        ifg.GroupName = r.ToString();
                        InformGroupList.Add(ifg);
                    }
                }
            }

            if (articleInfo == null)
                return PromptView("文章不存在");

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
                IsClassBrand = classInfo.IsClassBrand,
                MicroVideo = articleInfo.MicroVideo,
                ApprovalStatus = articleInfo.ApprovalStatus

                //MicroVideo = articleInfo.MicroVideo
            };
            if (articleInfo.IsClassBrand == 1)
            {
                model.IsClassBrand = articleInfo.IsClassBrand;
            }
            if (model.InformGroup.Contains(','))
            {
                string ingroup = model.InformGroup;
                string[] group = ingroup.Split(',');

                foreach (var item in InformGroupList)
                {
                    for (int i = 0; i < group.Length; i++)
                    {
                        if (item.GroupName.Equals(group[i]))
                        {
                            item.isSel = true;
                            break;
                        }
                        else
                        {
                            item.isSel = false;
                        }
                    }
                }


            }
            else
            {
                foreach (var item in InformGroupList)
                {
                    if (item.GroupName.Equals(model.InformGroup))
                    {
                        item.isSel = true;
                    }
                    else
                    {
                        item.isSel = false;
                    }
                }
            }
            model.InformGroupList = InformGroupList;
            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleInfo.ArticleClassID);
            ViewData["classPath"] = classPath;

            Load();

            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ArticleModel model, FormCollection collection, int articleid = -1)
        {
            string content = collection["bsEditor2"];
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);
            if (articleInfo == null)
                return PromptView("文章不存在");

            if (ModelState.IsValid)
            {
                if (model.ArticleClassID == 262 || model.ArticleClassID == 263 || model.DisplayType.Equals(2))
                {
                    try
                    {
                        string strGroups = collection.GetValue("checkboxGroup").AttemptedValue;//AttemptedValue返回一个以，分割的字符串
                        articleInfo.InformGroup = strGroups;
                    }
                    catch
                    {
                        return PromptView("/admin/Article/Edit?articleid=" + articleid + "", "请选择分组!");
                    }
                    DataTable ClassList = Article.GetArtClaIdByParentArticleClassIDs(model.ArticleClassID);
                    if (ClassList != null && ClassList.Rows.Count > 0)
                    {
                        return PromptView("该分类下有子分类，请选择相应的子分类");
                    }
                    ////如果改变文章所属分类
                    //if (articleInfo.ArticleClassID != model.ArticleClassID)
                    //{
                    //    //获取改变的栏目的类型
                    //    ArticleClassInfo ArticleClassInfo01 = ArticleClass.AdminGetModelById(model.ArticleClassID);
                    //    if (articleInfo.DisplayType != ArticleClassInfo01.ClassType)
                    //    {
                    //        if (articleInfo.DisplayType == 4 || ArticleClassInfo01.ClassType == 4)
                    //            return PromptView("分类类型不同，无法转移");
                    //    }
                    //}
                    articleInfo.ArticleClassID = model.ArticleClassID;
                    articleInfo.Title = model.Title;
                    articleInfo.SpecialID = model.SpecialID;
                    //articleInfo.DisplayType = model.DisplayType;
                    articleInfo.Url = model.Url == null ? "" : model.Url;
                    articleInfo.Digest = model.Digest;
                    articleInfo.ImgUrl = model.ImgUrl;
                    if (articleInfo.DisplayType == 2)
                    {
                        articleInfo.Body = content;
                    }
                    else
                    {
                        articleInfo.Body = model.Body ?? "";
                    }
                    articleInfo.Author = model.Author;
                    articleInfo.ComeForm = model.ComeForm;
                    articleInfo.IsShow = model.IsShow;
                    articleInfo.IsHome = model.IsHome;
                    articleInfo.IsBest = model.IsBest;
                    articleInfo.IsTop = model.IsTop;
                    articleInfo.AddTime = model.AddTime;
                    articleInfo.UpdateTime = DateTime.Now;
                    articleInfo.InformType = model.InformType;
                    articleInfo.EndTime = model.EndTime;
                    articleInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                    articleInfo.Description = model.Description == null ? "" : model.Description;
                    articleInfo.IsClassBrand = model.IsClassBrand;
                    articleInfo.MicroVideo = model.MicroVideo;
                    articleInfo.PushStatus = 0;

                    //如果上传视频但是没有上传题图，截取一帧图片作为题图
                    if (string.IsNullOrEmpty(model.ImgUrl) && !string.IsNullOrEmpty(model.MicroVideo))
                    {

                        articleInfo.ImgUrl = ValidateHelper.CatchImg(model.MicroVideo);
                    }
                    //如果没有添加摘要，读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        articleInfo.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    //如果是管理员添加的文章，直接通过审核
                    if (partUserInfo.AdminGroupID == 1)
                    {
                        articleInfo.ApprovalStatus = 1;
                    }
                    else
                    {
                        articleInfo.ApprovalStatus = model.ApprovalStatus;
                    }
                    if (string.IsNullOrEmpty(articleInfo.ImgUrl))
                    {
                        string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                        if (ImgUrls.Length > 0)
                        {
                            articleInfo.ImgUrl = ImgUrls[0];
                        }
                    }
                    Article.Update(articleInfo);
                    //班牌新闻推送
                    if (model.IsClassBrand > 0)
                    {
                        if (!sendClassBrand(articleid).Equals("发送失败"))
                        {
                            articleInfo.PushStatus = 1;
                            Article.Update(articleInfo);
                            return PromptView("班牌新闻推送成功");
                        }
                        else
                        {
                            articleInfo.PushStatus = 2;
                            Article.Update(articleInfo);
                            return PromptView("班牌新闻推送失败,保存新闻");
                        }
                    }
                    return PromptView("班牌通知修改成功");
                }
                else
                {
                    DataTable ClassList = Article.GetArtClaIdByParentArticleClassIDs(model.ArticleClassID);
                    if (ClassList != null && ClassList.Rows.Count > 0)
                    {
                        return PromptView("该分类下有子分类，请选择相应的子分类");
                    }
                    ////如果改变文章所属分类
                    //if (articleInfo.ArticleClassID != model.ArticleClassID)
                    //{
                    //    //获取改变的栏目的类型
                    //    ArticleClassInfo ArticleClassInfo01 = ArticleClass.AdminGetModelById(model.ArticleClassID);
                    //    if (articleInfo.DisplayType != ArticleClassInfo01.ClassType)
                    //    {
                    //        if (articleInfo.DisplayType == 4 || ArticleClassInfo01.ClassType == 4)
                    //            return PromptView("分类类型不同，无法转移");
                    //    }
                    //}
                    articleInfo.ArticleClassID = model.ArticleClassID;
                    articleInfo.Title = model.Title;
                    articleInfo.SpecialID = model.SpecialID;
                    // articleInfo.DisplayType = model.DisplayType;
                    articleInfo.Url = model.Url == null ? "" : model.Url;
                    articleInfo.Digest = model.Digest;
                    articleInfo.ImgUrl = model.ImgUrl;
                    articleInfo.Body = model.Body ?? "";
                    articleInfo.Author = model.Author;
                    articleInfo.ComeForm = model.ComeForm;
                    articleInfo.IsShow = model.IsShow;
                    articleInfo.IsHome = model.IsHome;
                    articleInfo.IsBest = model.IsBest;
                    articleInfo.IsTop = model.IsTop;
                    articleInfo.AddTime = model.AddTime;
                    articleInfo.UpdateTime = DateTime.Now;
                    articleInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                    articleInfo.Description = model.Description == null ? "" : model.Description;
                    articleInfo.IsClassBrand = model.IsClassBrand;
                    articleInfo.MicroVideo = model.MicroVideo;

                    //如果上传视频但是没有上传题图，截取一帧图片作为题图
                    if (string.IsNullOrEmpty(model.ImgUrl) && !string.IsNullOrEmpty(model.MicroVideo))
                    {

                        articleInfo.ImgUrl = ValidateHelper.CatchImg(model.MicroVideo);
                    }
                    //如果没有添加摘要，读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        articleInfo.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    //如果是管理员添加的文章，直接通过审核
                    if (partUserInfo.AdminGroupID == 1)
                    {
                        articleInfo.ApprovalStatus = 1;
                    }
                    else
                    {
                        articleInfo.ApprovalStatus = 0;
                    }
                    Article.Update(articleInfo);
                    AddLog(articleInfo, "文章修改成功");
                    return PromptView("文章修改成功");
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int articleClassId, string keyWord)
        {
            //班牌专栏显示隐藏
            ArticleClassInfo classInfo = ArticleClass.AdminGetModelById(articleClassId);

            ArticleModel model = new ArticleModel() { AddTime = DateTime.Now, UpdateTime = DateTime.Now, EndTime = DateTime.Now, IsShow = 1 };
            List<BonSite.Core.ArticleInfo.InformGroupItem> InformGroupList = new List<BonSite.Core.ArticleInfo.InformGroupItem>();

            if (BSConfig.SysConfig.EnableIpCard)
            {
                //string url = "10.0.64.176:2013/api/noticeSys/loadGroup";
                string url = BSConfig.SysConfig.IpCard + "/loadGroup";
                string ss = WebHelper.PostWebReq(url);
                if (!string.IsNullOrEmpty(ss))
                {
                    //解析json
                    JObject jo = JObject.Parse(ss);
                    string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
                    string grup = values[0];
                    string[] grups = grup.Split(',');
                    foreach (string r in grups)
                    {
                        ArticleInfo.InformGroupItem ifg = new ArticleInfo.InformGroupItem();
                        ifg.GroupName = r.ToString();
                        InformGroupList.Add(ifg);
                    }
                    model.InformGroupList = InformGroupList;
                }
            }


            if (articleClassId == 262)
            {
                model.DisplayType = 3;
            }
            else if (articleClassId == 263)
            {
                model.DisplayType = 2;
            }
            else if (classInfo.ClassType == 4)
            {
                model.DisplayType = 4;
            }
            else
            {
                model.DisplayType = 0;
            }
            model.IsClassBrand = classInfo.IsClassBrand;

            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleClassId);
            ViewData["classPath"] = classPath;

            Load();
            string ck = WebHelper.GetCookie("bs", "uname");
            model.Author = ck;
            model.ComeForm = "郑州八中";
            return View(model);
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(ArticleModel model, FormCollection collection)
        {
            //获取前台页面中的值
            string urlConvert = HttpContext.Request.Form["saveImg"];
            if (urlConvert == "on")
            {
                //处理地址转换
                model.Body = CommonHelper.DownloadImageConvertURL(model.Body);
            }
            ArticleInfo articleInfo = new ArticleInfo();
            string content = collection["bsEditor2"];
            //获取当前登录用户信息
            string ck = WebHelper.GetCookie("bs", "uname");
            PartUserInfo partUserInfo = Users.GetPartUserByName(ck);
            if (ModelState.IsValid)
            {
                string temp = string.Empty;
                //model.DisplayType
                if (model.ArticleClassID == 262 || model.ArticleClassID == 263)
                {
                    try
                    {
                        model.InformGroup = collection.GetValue("checkboxGroup").AttemptedValue;
                    }
                    catch
                    {
                        return PromptView("/admin/Article/Add?articleClassId=" + model.ArticleClassID + "&keyWord=0", "请选择分组!");
                    }
                    //如果是管理员添加的文章，直接通过审核
                    if (partUserInfo.AdminGroupID == 1)
                    {
                        articleInfo.ApprovalStatus = 1;
                    }
                    else
                    {
                        articleInfo.ApprovalStatus = 0;
                    }
                    articleInfo.ArticleClassID = model.ArticleClassID;
                    articleInfo.Title = model.Title;
                    articleInfo.SpecialID = model.SpecialID;
                    articleInfo.DisplayType = model.DisplayType;
                    articleInfo.Url = model.Url == null ? "" : model.Url;
                    articleInfo.Digest = model.Digest;
                    articleInfo.ImgUrl = model.ImgUrl;
                    if (model.Body != null)
                    {
                        articleInfo.Body = model.Body ?? "";
                    }
                    else
                    {
                        articleInfo.Body = content;
                    }
                    if (Request.Form.Get("Author").ToString().Equals(""))
                    {
                        articleInfo.Author = ck;
                    }
                    else
                    {
                        articleInfo.Author = model.Author;
                    }
                    articleInfo.ComeForm = model.ComeForm;
                    articleInfo.IsShow = model.IsShow;
                    articleInfo.IsHome = model.IsHome;
                    articleInfo.IsBest = model.IsBest;
                    articleInfo.IsTop = model.IsTop;
                    articleInfo.UpdateTime = DateTime.Now;
                    articleInfo.AddTime = model.AddTime;
                    articleInfo.UserID = 0;
                    articleInfo.AdminID = WorkContext.Uid;
                    articleInfo.IsClassBrand = 0;
                    articleInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                    articleInfo.Description = model.Description == null ? "" : model.Description;
                    articleInfo.InformType = model.InformType;
                    articleInfo.EndTime = model.EndTime;
                    articleInfo.InformGroup = model.InformGroup;
                    articleInfo.IsClassBrand = model.IsClassBrand;
                    articleInfo.MicroVideo = model.MicroVideo;
                    //该分类下有子分类，请选择相应的子分类
                    DataTable ClassList = Article.GetArtClaIdByParentArticleClassIDs(model.ArticleClassID);
                    if (ClassList != null && ClassList.Rows.Count > 0)
                    {
                        return PromptView("该分类下有子分类，请选择相应的子分类");
                    }

                    //如果没有添加摘要，读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        articleInfo.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    if (string.IsNullOrEmpty(articleInfo.ImgUrl))
                    {
                        string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                        if (ImgUrls.Length > 0)
                        {
                            articleInfo.ImgUrl = ImgUrls[0];
                        }
                    }
                    Article.Create(articleInfo);

                    //班牌新闻推送
                    if (model.IsClassBrand > 0)
                    {
                        if (!sendClassBrand(0).Equals("发送失败"))
                        {
                            int articleClassId = Article.GetArticleClassIdByIsClassBrand();
                            int articleid = Article.GetTopArticleIdByArticleClassId(articleClassId);
                            articleInfo.ArticleID = articleid;
                            articleInfo.PushStatus = 1;
                            Article.Update(articleInfo);
                            AddLog(articleInfo, "班牌新闻推送成功");
                            return PromptView("班牌新闻推送成功");
                        }
                        else
                        {
                            articleInfo.PushStatus = 0;
                            Article.Update(articleInfo);
                            AddLog(articleInfo, "班牌新闻推送失败");
                            return PromptView("班牌新闻推送失败,保存新闻");
                        }
                    }
                    return PromptView("班牌通知添加成功");


                }
                else
                {
                    articleInfo.ArticleClassID = model.ArticleClassID;
                    articleInfo.Title = model.Title;
                    articleInfo.SpecialID = model.SpecialID;
                    articleInfo.DisplayType = model.DisplayType;
                    articleInfo.Url = model.Url == null ? "" : model.Url;
                    articleInfo.Digest = model.Digest;
                    articleInfo.ImgUrl = model.ImgUrl;
                    articleInfo.Body = model.Body ?? "";
                    articleInfo.Author = model.Author;
                    articleInfo.ComeForm = model.ComeForm;
                    articleInfo.IsShow = model.IsShow;
                    articleInfo.IsHome = model.IsHome;
                    articleInfo.IsBest = model.IsBest;
                    articleInfo.IsTop = model.IsTop;
                    articleInfo.UpdateTime = DateTime.Now;
                    articleInfo.AddTime = model.AddTime;
                    articleInfo.UserID = 0;
                    articleInfo.AdminID = WorkContext.Uid;
                    articleInfo.IsClassBrand = 0;
                    articleInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                    articleInfo.Description = model.Description == null ? "" : model.Description;
                    articleInfo.InformType = model.InformType;
                    articleInfo.EndTime = model.EndTime;
                    articleInfo.InformGroup = model.InformGroup;
                    articleInfo.IsClassBrand = model.IsClassBrand;
                    articleInfo.PushStatus = model.PushStatus;
                    //该分类下有子分类，请选择相应的子分类
                    DataTable ClassList = Article.GetArtClaIdByParentArticleClassIDs(model.ArticleClassID);
                    if (ClassList != null && ClassList.Rows.Count > 0)
                    {
                        return PromptView("该分类下有子分类，请选择相应的子分类");
                    }
                    //如果没有添加摘要，读取文章前30个字
                    if (string.IsNullOrEmpty(model.Digest))
                    {
                        string ddd = BonSite.Core.CommonHelper.DelHTML(model.Body);
                        articleInfo.Digest = BonSite.Core.StringHelper.SubString(ddd, 30);
                    }
                    if (string.IsNullOrEmpty(model.ImgUrl) && !string.IsNullOrEmpty(model.MicroVideo))
                    {

                        articleInfo.ImgUrl = ValidateHelper.CatchImg(model.MicroVideo);
                    }
                    else
                    {
                        articleInfo.ImgUrl = model.ImgUrl;
                    }
                    if (string.IsNullOrEmpty(articleInfo.ImgUrl))
                    {
                        string[] ImgUrls = BonSite.Core.CommonHelper.GetHtmlImageUrlList(articleInfo.Body);
                        if (ImgUrls.Length > 0)
                        {
                            articleInfo.ImgUrl = ImgUrls[0];
                        }
                    }

                    articleInfo.MicroVideo = model.MicroVideo;
                    //如果是管理员添加的文章，直接通过审核
                    if (partUserInfo.AdminGroupID == 1)
                    {
                        articleInfo.ApprovalStatus = 1;
                    }
                    else
                    {
                        articleInfo.ApprovalStatus = 0;
                    }
                    Article.Create(articleInfo);
                    AddLog(articleInfo, "添加文章");

                    return PromptView("文章新加成功");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public  string GetHostAddress()
        {
            string userHostAddress = Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && CommonHelper.IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        public void AddLog(ArticleInfo articleInfo, string title)
        {
            string clientIp = Request.ServerVariables.Get("Remote_Addr").ToString();
            clientIp = GetHostAddress();

            CommonLog.AddLog(articleInfo, title, clientIp);
        }


        public ActionResult Del(int[] articleIdList)
        {
            foreach (var item in articleIdList)
            {
                ArticleInfo articleInfo = Article.GetModelByArticleID(Convert.ToInt32(item.ToString()));
                if (!string.IsNullOrEmpty(articleInfo.MicroVideo) && articleInfo.MicroVideo.Contains("ps"))
                {
                    string MicroVideo = System.AppDomain.CurrentDomain.BaseDirectory + @"upload\article\video\source\" + articleInfo.MicroVideo;
                    if (System.IO.File.Exists(MicroVideo))
                    {
                        //删除视频
                        System.IO.File.Delete(MicroVideo);
                        AddLog(articleInfo, "删除视频成功");
                    }
                }
                if (articleInfo.DisplayType.Equals(3))
                {
                    string url = "10.0.64.176:2013/api/noticeSys/deleteNotice";
                    StringBuilder param = new StringBuilder();
                    param.Append("noticeId=" + articleInfo.ArticleID.ToString());
                    param.Append("&noticeType=" + articleInfo.InformType);
                    string requ = WebHelper.SendRequestData(url, "post", param);
                    if (!requ.Equals("发送失败"))
                    {

                        Article.Del(articleIdList);
                        AddLog(articleInfo, "班牌发送删除内容--成功");
                        return PromptView("删除成功");
                    }
                    else
                    {
                        AddLog(articleInfo, "班牌发送删除内容--失败");
                        return PromptView("删除失败");
                    }
                }
                else
                {
                    Article.Del(articleIdList);
                    AddAdminOperateLog("删除内容", "删除内容,内容ID为:" + CommonHelper.IntArrayToString(articleIdList));
                    AddLog(articleInfo, "删除内容--成功");
                    return PromptView("删除成功");
                }
            }
            return PromptView("删除成功");
        }

        [HttpGet]
        public ActionResult Page(int articleClassID)
        {
            //首先判断本分类下是否有文章
            ArticleClassInfo articleClassInfo = ArticleClass.AdminGetModelById(articleClassID);

            int articleId = BonSite.Services.Article.GetArticleIdByArticleClassId(articleClassID);
            if (articleId == 0)
            {
                //创建新分类
                ArticleInfo info = new ArticleInfo()
                {
                    ArticleClassID = articleClassID,
                    SpecialID = 0,
                    Title = articleClassInfo.ClassName,
                    DisplayType = 0,
                    IsShow = 1,
                    IsTop = 0,
                    IsHome = 0,
                    IsBest = 0,
                    Body = articleClassInfo.ClassName + "的内容",
                    AddTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    Author = "",
                    ComeForm = "",
                    ImgUrl = "",
                    Url = "",
                    Digest = "",
                    Keys = "",
                    UserID = 0,
                    AdminID = 0,
                    IsClassBrand = 0
                };
                articleId = Article.Create(info);
            }

            ArticleInfo articleInfo = Article.GetModelByArticleID(articleId);

            ArticleModel model = new ArticleModel()
            {
                ArticleClassID = articleInfo.ArticleClassID,
                Title = articleInfo.Title,
                SpecialID = articleInfo.SpecialID,
                DisplayType = articleInfo.DisplayType,
                Url = articleInfo.Url,
                Digest = articleInfo.Digest,
                Body = articleInfo.Body,
                Author = articleInfo.Author,
                ComeForm = articleInfo.ComeForm,
                IsShow = articleInfo.IsShow,
                IsHome = articleInfo.IsHome,
                IsBest = articleInfo.IsBest,
                IsTop = articleInfo.IsTop,
                Keyword = articleInfo.Keyword,
                Description = articleInfo.Description
            };

            List<ArticleClassInfo> classPath = ArticleClass.GetArticleClassPath(articleInfo.ArticleClassID);
            ViewData["classPath"] = classPath;

            Load();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Page(ArticleModel model, int articleClassId)
        {
            int articleid = BonSite.Services.Article.GetArticleIdByArticleClassId(articleClassId);
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);
            if (articleInfo == null)
                return PromptView("文章不存在");

            if (ModelState.IsValid)
            {

                articleInfo.ArticleClassID = model.ArticleClassID;
                articleInfo.Title = model.Title;
                articleInfo.SpecialID = model.SpecialID;
                articleInfo.DisplayType = model.DisplayType;
                articleInfo.Url = model.Url == null ? "" : model.Url;
                articleInfo.Digest = model.Digest;
                articleInfo.Body = model.Body ?? "";
                articleInfo.Author = model.Author;
                articleInfo.ComeForm = model.ComeForm;
                articleInfo.IsShow = 1;
                articleInfo.IsHome = model.IsHome;
                articleInfo.IsBest = model.IsBest;
                articleInfo.IsTop = model.IsTop;
                articleInfo.UpdateTime = DateTime.Now;
                articleInfo.Keyword = model.Keyword == null ? "" : model.Keyword;
                articleInfo.Description = model.Description == null ? "" : model.Description;
                Article.Update(articleInfo);
                //AddAdminOperateLog
                AddLog(articleInfo, "文章修改--成功");
                return PromptView("修改成功");
            }
            return View(model);
        }
        //发送到电子班牌
        public ActionResult send(int articleid)
        {

            // string url = "10.0.64.176:2013/api/noticeSys/updateNotice";


            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);
            StringBuilder param = new StringBuilder();
            param.Append("noticeId=" + articleInfo.ArticleID.ToString());
            param.Append("&noticeType=" + articleInfo.InformType);
            param.Append("&title=" + articleInfo.Title);
            param.Append("&content=" + articleInfo.Digest);
            param.Append("&endTime=" + articleInfo.EndTime.ToString("yyyy-MM-dd   HH:mm"));
            param.Append("&group=" + articleInfo.InformGroup);
            //param.Append("&group=''");
            param.Append("&picData=" + articleInfo.ImgUrl);

            if (BSConfig.SysConfig.EnableIpCard)
            {
                string url = BSConfig.SysConfig.IpCard + "/updateNotice";

                string requ = WebHelper.SendRequestData(url, "post", param);
                if (!requ.Equals("发送失败"))
                {
                    articleInfo.PushStatus = 1;
                    Article.Update(articleInfo);
                    AddLog(articleInfo, "文章修改 班牌发送--成功");
                    return PromptView("发送成功");
                }
                else
                {
                    articleInfo.PushStatus = 2;
                    Article.Update(articleInfo);
                    AddLog(articleInfo, "文章修改 班牌发送--失败");
                    return PromptView("发送失败");
                }
            }
            else
            {
                return PromptView("发送失败没有开启发送班牌");
            }

        }
        //推送班牌新闻
        public string sendClassBrand(int articleid)
        {
            //string url = "218.28.141.90:2013/api/noticeSys/updateNotice";
            // string url = "http://10.0.64.176:2013/api/noticeSys/updateNews";
            if (articleid == 0)
            {
                int articleClassId = Article.GetArticleClassIdByIsClassBrand();
                articleid = Article.GetTopArticleIdByArticleClassId(articleClassId);
            }
            ArticleInfo articleInfo = Article.GetModelByArticleID(articleid);

            StringBuilder param = new StringBuilder();
            //param.Append("noticeId=" + articleInfo.ArticleID.ToString());
            ////param.Append("noticeId=3338");
            ////param.Append("&noticeType=1");

            //param.Append("&noticeType=" + articleInfo.InformType);
            //param.Append("&title=" + articleInfo.Title);
            //param.Append("&content=" + articleInfo.Digest);
            //param.Append("&endTime=" + articleInfo.EndTime.ToString("yyyy-MM-dd   HH:mm"));
            //param.Append("&group=" + articleInfo.InformGroup);
            //param.Append("&picData=" + articleInfo.ImgUrl);
            SiteConfigInfo siteConfigInfo = BSConfig.SiteConfig;
            string zzbz_host = siteConfigInfo.SiteUrl;
            param.Append("newsId=" + articleid + "");
            param.Append("&title=" + articleInfo.Title + "");
            param.Append("&newsUrl=" + zzbz_host + "ClassBrand/View?id=" + articleid + "");
            param.Append("&group=" + articleInfo.InformGroup + "");

            string requ = "";
            if (BSConfig.SysConfig.EnableIpCard)
            {
                string url = BSConfig.SysConfig.IpCard + "/updateNews";
                requ = WebHelper.SendRequestData(url, "post", param);

                AddLog(articleInfo, "推送班牌新闻 班牌发送" + requ);
            }
            return requ;
        }
        //班牌新闻预览
        public ActionResult ClassBrand(int id)
        {
            ArticleInfo info = Article.GetModelByArticleID(id);
            if (info == null)
                return PromptView("/Null", "链接失效!");

            //if (info.IsShow == 0)
            //    return PromptView("/", "您访问的页面不存在");
            ArticleModel model = new ArticleModel();
            model.Title = info.Title;
            model.ComeForm = info.ComeForm;
            model.Author = info.Author;
            model.AddTime = info.AddTime;
            model.Body = info.Body;
            model.ImgUrl = info.ImgUrl;
            return View(model);
        }
        //文章预览
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
        //班牌新闻删除接口
        public ActionResult classBrandDel(int id)
        {
            // string url = "http://10.0.64.176:2013/api/noticeSys/deleteNews";
            if (id > 0)
            {
                ArticleInfo articleInfo = Article.GetModelByArticleID(id);
                if (articleInfo != null)
                {
                    StringBuilder param = new StringBuilder();
                    param.Append("newsId=" + id + "");

                    if (BSConfig.SysConfig.EnableIpCard)
                    {
                        string url = BSConfig.SysConfig.IpCard + "/deleteNews";
                        string requ = WebHelper.SendRequestData(url, "post", param);
                        if (requ.Contains("ok"))
                        {
                            articleInfo.InformGroup = "";
                            articleInfo.PushStatus = 0;
                            Article.Update(articleInfo);
                            AddLog(articleInfo, "撤销班牌显示成功");
                            return PromptView("撤销班牌显示成功");
                        }
                    }
                    else
                    {
                        AddLog(articleInfo, "没有启用发送班牌撤销班牌显示成功");
                        return PromptView("没有启用发送班牌撤销班牌显示成功");
                    }
                }
            }

            return PromptView("撤销班牌显示失败");
        }
        // / <summary>
        //// 初始化，载入下拉菜单等
        //// </summary>
        public void Load()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem() { Text = "顶级分类", Value = "0" });
            //bool isHaveChild = ArticleClass.HaveChild(articleClassId);
            //foreach (ArticleClassInfo info in ArticleClass.AdminGetArticleClassTreeList(articleClassId, isHaveChild))
            //{
            //    list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            //}
            foreach (ArticleClassInfo info in ArticleClass.AdminGetArticleClassTreeList())
            {
                list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            }

            List<SelectListItem> slist = new List<SelectListItem>();
            slist.Add(new SelectListItem() { Text = "不属于任何专题", Value = "0" });
            foreach (SpecialInfo info in Special.AdminGetList())
            {
                slist.Add(new SelectListItem() { Text = info.Title, Value = info.SpecialID.ToString() });
            }

            ViewData["articleClassList"] = list;
            ViewData["specialList"] = slist;

            string allowImgType = string.Empty;
            string allowVideoType = string.Empty;

            string[] imgTypeList = StringHelper.SplitString(BSConfig.SiteConfig.UploadImgType, "@");
            string imageType = imgTypeList[0];
            string videoType = imgTypeList[1];

            foreach (string imgType in imageType.Split(','))
                allowImgType += string.Format("*{0};", imgType.ToLower());
            ViewData["allowImgType"] = allowImgType;


            foreach (string imgType in videoType.Split(','))
                allowVideoType += string.Format("*{0};", imgType.ToLower());
            ViewData["allowVideoType"] = allowVideoType;

            string[] sizeList = StringHelper.SplitString(WorkContext.SiteConfig.ArticleImgThumbSize);
            ViewData["size"] = sizeList[sizeList.Length / 2];

            ViewData["maxImgSize"] = BSConfig.SiteConfig.UploadImgSize;
            ViewData["referer"] = SiteUtils.GetAdminRefererCookie();

        }


        public ActionResult LogShow(int pageSize = 15, int pageNumber = 1)
        {

            //modellist
            ArticleListModel model = new ArticleListModel();
            //获取log中的数据

            PageModel pageModel = new PageModel(pageSize, pageNumber, Log.Count());

            model.DataList = Log.QueryLog(pageSize, pageNumber, "", " id desc ");

            ArticleClassInfo articleClassInfo = new ArticleClassInfo();
            articleClassInfo.ClassName = "日志查询";
            model.ClassInfo = articleClassInfo;

            model.PageModel = pageModel;

            return View(model);
        }

        public ActionResult SearchPage(int pageSize = 15, int pageNumber = 1) {
            //modellist
            ArticleListModel model = new ArticleListModel();
            //获取log中的数据
            string tempLike = Request["search"];
            string condition = " title like '%"+ tempLike + "%'";

            PageModel pageModel = new PageModel(pageSize, pageNumber, Article.AdminGetArticleCount(condition));

            model.DataList = Article.AdminGetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, " addTime desc");

            ArticleClassInfo articleClassInfo = new ArticleClassInfo();
            articleClassInfo.ClassName = "数据查询";
            model.ClassInfo = articleClassInfo;

            model.PageModel = pageModel;
            return View(model);
        }

    }
}
