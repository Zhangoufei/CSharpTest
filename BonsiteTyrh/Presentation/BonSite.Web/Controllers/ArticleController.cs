using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BonSite.Core;
using BonSite.Services;
using BonSite.Web.Framework;
using BonSite.Web.Models;
using System.Text;
using System.Data;

namespace BonSite.Web.Controllers
{
    public class ArticleController : BaseWebController
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            string key = WebHelper.GetQueryString("key");
            if (string.IsNullOrWhiteSpace(key))
                return PromptView("/", "请填写搜索关键字！");

            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");



            string condition = string.Format("isshow=1 and title like '%{0}%'", key);
            string sort = Article.GetArticleListSort("", "");

            PageModel pageModel = new PageModel(20, page, Article.GetArticleCount(condition));


            SearchListModel model = new SearchListModel
            {
                key = key,
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };
            return View(model);

        }

        /// <summary>
        /// 专题
        /// </summary>
        /// <returns></returns>
        public ActionResult Special()
        {
            int specialid = GetRouteInt("id");
            if (specialid == 0)
                specialid = WebHelper.GetQueryInt("id");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            SpecialInfo info = BonSite.Services.Special.GetModelById(specialid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            //外部链接
            if (info.IsOut.Equals(1))
                return Redirect(info.OutUrl);


            string condition = string.Format("isshow=1 and Specialid = {0}", specialid);
            string sort = Article.GetArticleListSort("", "");

            PageModel pageModel = new PageModel(20, page, Article.GetArticleCount(condition));



            SpecialListModel model = new SpecialListModel
            {
                SpecialId = specialid,
                SpecialInfo = info,
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };

            if (info.IsOut.Equals(2))
                return View("special." + specialid, model);
            else
                return View(model);
        }


        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int classid = GetRouteInt("classid");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("classid");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");
            //总页数
            int totalcount = 0;
            //单页数据
            int pageSize = 0;
            //页码
            int pageNum = 1;
            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            //无下级菜单的列表不显示头部
            string isNotDisplay = "";
            //栏目id
            int acticleClassId = 0;
            //文章类型
            int ClassType = 2;
            if (!ArticleClass.HaveChild(info.ArticleClassID))
            {
                isNotDisplay = "display:none";
            }
            ViewData["isNotDisplay"] = isNotDisplay;

            if (info == null)
                return PromptView("/", "您访问的页面不存在");
            ClassType = info.ClassType;
            //外部链接
            if (info.ClassType.Equals(0))
                return Redirect(info.WebUrl);
            //判断菜单栏是不是单页和菜单是否有下级菜单  || !ArticleClass.HaveChild(info.ArticleClassID)          
            if (info.ClassType.Equals(1))
            {
                //单页
                int articleid = Article.GetArticleIdByArticleClassId(info.ArticleClassID);
                ViewData["ClassArticleIds"] = classid;
                if (articleid > 0)
                    return RedirectToAction("Details", new { id = articleid, ClassArticleId = classid });
                else
                    return PromptView("/", "您访问的页面不存在");
            }
            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classid);
            ViewData["list"] = list;
            //默认三级分类中第一条相关数据
            if (list.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(list.Rows[0]["ArticleClassID"].ToString()))
                {
                    classid = int.Parse(list.Rows[0]["ArticleClassID"].ToString());
                }
                if (!string.IsNullOrEmpty(list.Rows[0]["ClassType"].ToString()))
                {
                    ClassType = int.Parse(list.Rows[0]["ClassType"].ToString());
                }
            }
            else
            {
                acticleClassId = classid;

            }
            if (ClassType.Equals(4))
            {
                pageSize = 8;
            }
            else if (ClassType.Equals(3))
            {
                pageSize = 8;
            }
            else
            {
                pageSize = 8;
            }
            int m = Article.GetArticleList(classid, 100000000).Count % pageSize;
            if (m > 0)
            {
                totalcount = Article.GetArticleList(classid, 100000000).Count / pageSize + 1;
            }
            else
            {
                totalcount = Article.GetArticleList(classid, 100000000).Count / pageSize;
            }
            ViewData["acticleClassId"] = classid;
            ViewData["ClassType"] = ClassType;
            ViewData["totalcount"] = totalcount;
            DataTable namelist = Article.GetNameByArticleClassID(info.ParentArticleClassID);
            ViewData["namelist"] = namelist;

            string condition = Article.GetArticleListCondition(classid, "");
            string sort = Article.GetArticleListSort("", "");

            PageModel pageModel = new PageModel(20, page, Article.GetArticleCount(condition));



            ArticleListModel model = new ArticleListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(classid),
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };


            if (info.ListView.Length > 0)
                return View("List." + info.ListView, model);
            else
                return View(model);

        }
        JsonResult json = new JsonResult();
        [HttpPost]
        public JsonResult JsonArticle()
        {    //默认文章类型为文章列表2,图文列表是-1
            int ClassType = 2;
            int PageSize = 8;
            //总页数
            int totalcount = 0;
            //当前页
            int PageNumber = 1;
            string acticleClassId = HttpContext.Request.Form["acticleClassId"];
            string pindex = HttpContext.Request.Form["pindex"];
            string Class_Type = HttpContext.Request.Form["ClassType"];
            string totalcounts = HttpContext.Request.Form["totalcounts"];
            if (!string.IsNullOrEmpty(pindex))
            {
                PageNumber = int.Parse(pindex);
            }
            if (!string.IsNullOrEmpty(Class_Type))
            {
                ClassType = int.Parse(Class_Type);
            }
            //if (ClassType.Equals(4))
            //{
            //    PageSize = 8;
            //}
            //else if (ClassType.Equals(3))
            //{
            //    PageSize = 8;
            //}
            PageSize = 8;
            //判断是否获取数据
            if (string.IsNullOrEmpty(acticleClassId))
            {
                json.Data = new { code = 0, msg = "暂无数据" };
                return json;
            }
            ArticleClassInfo info = ArticleClass.GetModelById(int.Parse(acticleClassId));
            string condition = Article.GetArticleListCondition(int.Parse(acticleClassId), "");
            string sort = Article.GetArticleListSort("", "");
            ClassType = info.ClassType;
            //PageModel pageModel = new PageModel(20, page, Article.GetArticleCount(condition));
            ArticleListModel model = new ArticleListModel
            {
                ArticleClassID = int.Parse(acticleClassId),
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(int.Parse(acticleClassId)),
                ArticleList = Article.GetArticleList(PageNumber * PageSize, 1, condition, sort),
                //PageModel = pageModel
            };
            //总页数 
            if (!string.IsNullOrEmpty(totalcounts))
            {
                totalcount = int.Parse(totalcounts);
            }
            else
            {
                int m = Article.GetArticleList(int.Parse(acticleClassId), 100000000).Count % PageSize;
                int pageCount = 1;
                if (m > 0)
                {
                    totalcount = Article.GetArticleList(int.Parse(acticleClassId), 100000000).Count / PageSize + 1;
                }
                else
                {
                    totalcount = Article.GetArticleList(int.Parse(acticleClassId), 100000000).Count / PageSize;
                }
            }
            // model.ArticleList = Article.GetArticleList(8, 1, condition, sort);
            //获取最新7条资讯+7条历史资讯  最新小于等于14
            ArticleListModel newArticlelistmodel = new ArticleListModel
            {
                ArticleClassID = int.Parse(acticleClassId),
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(int.Parse(acticleClassId)),
                ArticleList = Article.GetArticleList(PageNumber * PageSize, 1, condition, sort),
                //PageModel = pageModel
            };
            //最新小于等于14资讯转化列表
            var newArticleList = from p in newArticlelistmodel.ArticleList.AsEnumerable()
                                 select new
                                 {
                                     ImgUrl = p["ImgUrl"].ToString(),
                                     Title = p["Title"].ToString(),
                                     Digest = p["Digest"].ToString(),
                                     UpdateTime = Convert.ToDateTime(p["AddTime"].ToString()).ToString("MM/dd"),
                                     ArticleID = p["ArticleID"].ToString(),
                                     ArticleClassID = p["ArticleClassID"].ToString()
                                 };
            ////最新7条资讯
            // List<ArticleInfo> newArticleList = Article.GetArticleList(int.Parse(acticleClassId), 7);
            ////获取最多7条历史资讯
            // List<ArticleInfo> historyArticleList = Article.GetArticleList(int.Parse(acticleClassId), 7);
            //图文列表
            var ArticleList = from p in model.ArticleList.AsEnumerable()
                              select new
                              {
                                  ImgUrl = p["ImgUrl"].ToString(),
                                  Title = p["Title"].ToString(),
                                  Digest = p["Digest"].ToString(),
                                  UpdateTime = Convert.ToDateTime(p["AddTime"].ToString()).ToString("MM/dd"),
                                  ArticleID = p["ArticleID"].ToString(),
                                  ArticleClassID = p["ArticleClassID"].ToString()
                              };
            if (ClassType.Equals(3))
            {

                json.Data = new { code = 1, msg = "success", ArticleList = ArticleList, count = ArticleList.ToList<object>().Count, ArticleClassImgUrl = model.ArticleClassInfo.ImgUrl, ClassType = ClassType, ClassName = model.ArticleClassInfo.ClassName, totalcount = totalcount, PageNumber = PageNumber };
                return json;
            }
            else if (ClassType.Equals(2))
            {
                //返回最新资讯和历史资讯，
                json.Data = new { code = 1, msg = "success", ArticleList = newArticleList, count = newArticleList.ToList<object>().Count, ArticleClassImgUrl = model.ArticleClassInfo.ImgUrl, ClassType = ClassType, ClassName = model.ArticleClassInfo.ClassName, totalcount = totalcount, PageNumber = PageNumber };
                return json;
            }
            else if (ClassType.Equals(4))
            {
                //返回微视频列表              
                json.Data = new { code = 1, msg = "success", ArticleList = ArticleList, count = ArticleList.ToList<object>().Count, ArticleClassImgUrl = model.ArticleClassInfo.ImgUrl, ClassType = ClassType, ClassName = model.ArticleClassInfo.ClassName, totalcount = totalcount, PageNumber = PageNumber };
                return json;
            }
            else
            {
                json.Data = new { code = 0, msg = "暂无数据" };
                return json;
            }
        }
        /// <summary>
        /// 文章详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(string ClassArticleId)
        {
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");
            //新闻列表
            string ClassArticleIds = "";
            if (ClassArticleId == null)
            {
                ClassArticleIds = "";
            }
            else
            {
                ClassArticleIds = ClassArticleId;
            }
            ViewData["ClassArticleIds"] = ClassArticleIds;
            ArticleInfo info = Article.GetModelByArticleID(id);
            ArticleClassInfo articleClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            if (info.IsShow == 0)
                return PromptView("/", "您访问的页面不存在");
            //判断文章是否是微视频
            //info.DisplayType = 5; //微视频布局测试
            if (info.DisplayType.Equals(4))
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
            if (info.DisplayType.Equals(1))
                return Redirect(info.Url);
            ArticleModel model = new ArticleModel
            {
                ArticleInfo = info,
                ArticleClassInfo = ArticleClass.GetModelById(info.ArticleClassID),
                ClssPath = ArticleClass.GetArticleClassPath(info.ArticleClassID)
            };
            //if (model.ArticleInfo.DisplayType.Equals(2))
            //    return View("details.info." + model.ArticleInfo.ArticleID, model);
            //else if (model.ArticleClassInfo.ContentView.Length > 0)
            //    return View("details." + model.ArticleClassInfo.ContentView, model);
            //else
            //访问量加一
            ArticleInfo info2 = Article.GetModelByArticleID(id);
            int Hits = Convert.ToInt32(info2.Hits) + 1;
            info2.Hits = Hits;
            Article.Update(info2);
            return View(model);

        }

        /// <summary>
        /// 在线投稿
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deliver()
        {

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ArticleClassInfo info in ArticleClass.GetOpenArticleClassList())
            {
                list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            }

            if (list.Count == 0)
                return PromptView("暂无开放投稿的栏目！");

            ViewData["articleClassList"] = list;
            DeliverModel model = new DeliverModel();

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Deliver(DeliverModel model)
        {
            if (ModelState.IsValid)
            {
                ArticleInfo info = new ArticleInfo
                {
                    AddTime = DateTime.Now,
                    AdminID = 0,
                    DisplayType = 0,
                    Hits = 0,
                    ImgUrl = "",
                    IsBest = 0,
                    IsHome = 0,
                    IsTop = 0,
                    Keys = "",
                    SpecialID = 0,
                    UpdateTime = DateTime.Now,
                    Url = "",
                    UserID = 0,
                    Title = model.Title,
                    ArticleClassID = model.ArticleClassID,
                    Body = model.Body,
                    Author = model.Author,
                    ComeForm = model.ComeForm,
                    IsShow = 0,
                    Digest = ""

                };
                Article.Create(info);

                return PromptView("稿件提交成功，请耐心等待管理员审核！");
            }


            List<SelectListItem> list = new List<SelectListItem>();
            foreach (ArticleClassInfo info in ArticleClass.AdminGetArticleClassTreeList())
            {
                list.Add(new SelectListItem() { Text = info.ClassName, Value = info.ArticleClassID.ToString() });
            }

            ViewData["articleClassList"] = list;
            return View(model);

        }
        //pc端列表页
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ListPc(string ClassArticleId)
        {
            int classid = GetRouteInt("classid");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("classid");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            //新闻列表
            string ClassArticleIds = "";
            if (ClassArticleId == null)
            {
                ClassArticleIds = "";
            }
            else
            {
                ClassArticleIds = ClassArticleId;
            }
            //声明1级、2级、3级菜单信息
            int ClassArticleId1 = 0;
            string ClassArticleIdName1 = "";
            int ClassArticleId2 = 0;
            string ClassArticleIdName2 = "";
            int ClassArticleId3 = 0;
            string ClassArticleIdName3 = "";
            //父级菜单id
            int ParentArticleClassID = 0;
            //二级菜单显示隐藏
            string HaveChild2 = "";
            //三级菜单
            string HaveChild3 = "";

            List<int> clsaaIdList = null;

            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            //当前菜单id
            ViewData["ClassArticleId"] = classid;
            //获取当前菜单的名称
            ViewData["ClassArticleName"] = info.ClassName;
            //三级菜单列表
            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classid);

            //判断是否有下级菜单 如果有肯定是当前肯定是二级

            if (ArticleClass.HaveChild(classid))
            {
                //当前是二级菜单，有三级菜单                
                //获取上级菜单的名称和id
                ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                if (Parentinfo != null)
                {
                    ClassArticleId1 = Parentinfo.ArticleClassID;
                    ClassArticleIdName1 = Parentinfo.ClassName;
                    ClassArticleId2 = classid;
                    ClassArticleIdName2 = info.ClassName;
                    HaveChild3 = "";


                    List<ArticleClassInfo> articleClassInfos = ArticleClass.AdminGetArticleClassTreeList(classid, true);
                    clsaaIdList = new List<int>();
                    foreach (var item in articleClassInfos)
                    {
                        clsaaIdList.Add(item.ArticleClassID);
                    }

                    ArticleClassInfo info2 = articleClassInfos[0];
                    classid = info2.ArticleClassID;
                    ClassArticleId3 = info2.ArticleClassID;
                    ClassArticleIdName3 = info2.ClassName;

                }
            }
            else
            {
                ParentArticleClassID = Article.GetNameByArticleClassID2(classid);
                if (ParentArticleClassID > 0)
                {
                    ParentArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
                    if (ParentArticleClassID > 0)
                    {
                        //没有子菜单，但是有2个上级菜单，说明当前是3级菜单

                        ClassArticleId3 = classid;
                        ClassArticleIdName3 = info.ClassName;
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId2 = Parentinfo.ArticleClassID;
                        ClassArticleIdName2 = Parentinfo.ClassName;
                        ClassArticleId1 = ParentArticleClassID;
                        ClassArticleIdName1 = ArticleClass.GetModelById(ParentArticleClassID).ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "";
                        //三级菜单列表
                        list = Article.GetArtClaIdByParentArticleClassIDs(info.ParentArticleClassID);
                    }
                    else
                    {
                        //没有子菜单，但是1个上级菜单，说明当前是二级菜单
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId1 = Parentinfo.ArticleClassID;
                        ClassArticleIdName1 = Parentinfo.ClassName;
                        ClassArticleId2 = classid;
                        ClassArticleIdName2 = info.ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "display:none";

                    }
                }
                else
                {
                    //没有子菜单，没有父级菜单，说明是1级菜单
                    ClassArticleId1 = classid;
                    ClassArticleIdName1 = info.ClassName;
                    ClassArticleId2 = 0;
                    ClassArticleIdName2 = "";
                    HaveChild2 = "display:none";
                    HaveChild3 = "display:none";

                }
            }
            ViewData["list"] = list;
            ViewData["HaveChild2"] = HaveChild2;
            ViewData["HaveChild3"] = HaveChild3;
            ViewData["ClassArticleId1"] = ClassArticleId1;
            ViewData["ClassArticleId2"] = ClassArticleId2;
            ViewData["ClassArticleId3"] = ClassArticleId3;
            ViewData["ClassArticleIdName1"] = ClassArticleIdName1;
            ViewData["ClassArticleIdName2"] = ClassArticleIdName2;
            ViewData["ClassArticleIdName3"] = ClassArticleIdName3;


            //外部链接
            if (info.ClassType == 0)
                return Redirect(info.WebUrl);

            if (info.ClassType.Equals(1))
            {
                //单页
                int articleid = Article.GetArticleIdByArticleClassId(info.ArticleClassID);
                ViewData["ClassArticleIds"] = classid;
                if (articleid > 0)
                    return RedirectToAction("DetailsPc", new { id = articleid, ClassArticleId = classid });
                else
                    return PromptView("/", "您访问的页面不存在");
            }

            if (info.ClassType.Equals(3))
            {
                return RedirectToAction("ListVideo", new { classid = classid, page = page });
            }
            else if (info.ClassType.Equals(4))
            {
                if (ClassArticleIds.Equals("297"))
                {
                    return RedirectToAction("ListVideo", new { classid = classid, page = page });
                }
                else
                {
                    return RedirectToAction("ListVideo2", new { classid = classid, page = page });
                }

            }
            string condition = "";
            if (clsaaIdList != null && clsaaIdList.Count > 1)
            {
                condition = Article.GetArticleListConditionList(clsaaIdList, "");
            }
            else
            {
                condition = Article.GetArticleListCondition(classid, "");
            }


            string sort = Article.GetArticleListSort("", "");

            PageModel pageModel = new PageModel(20, page, Article.GetArticleCount(condition));


            ArticleListModel model = new ArticleListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(classid),
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };


            //if (info.ListView.Length > 0)
            //    return View("List." + info.ListView, model);
            //else
            return View(model);
        }
        //带分页的视频列表
        public ActionResult ListVideo()
        {
            int classid = GetRouteInt("ClassId");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("ClassId");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");
            //声明1级、2级、3级菜单信息
            int ClassArticleId1 = 0;
            string ClassArticleIdName1 = "";
            int ClassArticleId2 = 0;
            string ClassArticleIdName2 = "";
            int ClassArticleId3 = 0;
            string ClassArticleIdName3 = "";
            //父级菜单id
            int ParentArticleClassID = 0;
            //二级菜单显示隐藏
            string HaveChild2 = "";
            //三级菜单
            string HaveChild3 = "";
            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");
            //三级菜单列表
            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classid);

            //判断是否有下级菜单 如果有肯定是当前肯定是二级

            if (ArticleClass.HaveChild(classid))
            {
                //当前是二级菜单，有三级菜单                
                //获取上级菜单的名称和id
                ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                if (Parentinfo != null)
                {
                    ClassArticleId1 = Parentinfo.ArticleClassID;
                    ClassArticleIdName1 = Parentinfo.ClassName;
                    ClassArticleId2 = classid;
                    ClassArticleIdName2 = info.ClassName;
                    HaveChild3 = "";
                    ArticleClassInfo info2 = ArticleClass.AdminGetArticleClassTreeList(classid, true)[0];
                    classid = info2.ArticleClassID;
                    ClassArticleId3 = info2.ArticleClassID;
                    ClassArticleIdName3 = info2.ClassName;

                }
            }
            else
            {
                ParentArticleClassID = Article.GetNameByArticleClassID2(classid);
                if (ParentArticleClassID > 0)
                {
                    ParentArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
                    if (ParentArticleClassID > 0)
                    {
                        //没有子菜单，但是有2个上级菜单，说明当前是3级菜单

                        ClassArticleId3 = classid;
                        ClassArticleIdName3 = info.ClassName;
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId2 = Parentinfo.ArticleClassID;
                        ClassArticleIdName2 = Parentinfo.ClassName;
                        ClassArticleId1 = ParentArticleClassID;
                        ClassArticleIdName1 = ArticleClass.GetModelById(ParentArticleClassID).ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "";
                        //三级菜单列表
                        list = Article.GetArtClaIdByParentArticleClassIDs(info.ParentArticleClassID);
                    }
                    else
                    {
                        //没有子菜单，但是1个上级菜单，说明当前是二级菜单
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId1 = Parentinfo.ArticleClassID;
                        ClassArticleIdName1 = Parentinfo.ClassName;
                        ClassArticleId2 = classid;
                        ClassArticleIdName2 = info.ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "display:none";

                    }
                }
                else
                {
                    //没有子菜单，没有父级菜单，说明是1级菜单
                    ClassArticleId1 = classid;
                    ClassArticleIdName1 = info.ClassName;
                    ClassArticleId2 = 0;
                    ClassArticleIdName2 = "";
                    HaveChild2 = "display:none";
                    HaveChild3 = "display:none";

                }
            }
            ViewData["list"] = list;
            ViewData["HaveChild2"] = HaveChild2;
            ViewData["HaveChild3"] = HaveChild3;
            ViewData["ClassArticleId1"] = ClassArticleId1;
            ViewData["ClassArticleId2"] = ClassArticleId2;
            ViewData["ClassArticleId3"] = ClassArticleId3;
            ViewData["ClassArticleIdName0"] = ClassArticleIdName1;
            ViewData["ClassArticleIdName2"] = ClassArticleIdName2;
            ViewData["ClassArticleIdName3"] = ClassArticleIdName3;
            if (info.ClassType.Equals(3))
            {
                ViewData["video"] = "display:none";
                ViewData["content-video-meng"] = "";
            }
            else
            {
                ViewData["content-video-meng"] = "content-video-meng";
                ViewData["video"] = "";
            }
            string condition = Article.GetArticleListCondition(classid, "");
            string sort = Article.GetArticleListSort("", "");
            PageModel pageModel = new PageModel(8, page, Article.GetArticleCount(condition));
            ViewData["ClassArticleIdName1"] = info.ClassName;
            ArticleListModel model = new ArticleListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(classid),
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };
            //if (info.ListView.Length > 0)
            //    return View("List." + info.ListView, model);
            //else
            return View(model);
        }
        //pc详情页
        public ActionResult DetailsPc(string ClassArticleId)
        {
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");
            //新闻列表
            string ClassArticleIds = "";
            if (ClassArticleId == null)
            {
                ClassArticleIds = "";
            }
            else
            {
                ClassArticleIds = ClassArticleId;
            }
            ViewData["ClassArticleIds"] = ClassArticleIds;
            ArticleInfo info = Article.GetModelByArticleID(id);
            ArticleClassInfo articleClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            if (info.IsShow == 0)
                return PromptView("/", "您访问的页面不存在");
            if (info.DisplayType.Equals(4))
            {
                return RedirectToAction("videoshows", new { id = id, ClassArticleId = ClassArticleIds });
            }
            //声明1级、2级、3级菜单信息
            int ClassArticleId1 = 0;
            string ClassArticleIdName1 = "";
            int ClassArticleId2 = 0;
            string ClassArticleIdName2 = "";
            int ClassArticleId3 = 0;
            string ClassArticleIdName3 = "";
            //父级菜单id
            int ParentArticleClassID = 0;
            //二级菜单显示隐藏
            string HaveChild2 = "";
            //三级菜单
            string HaveChild3 = "";

            ArticleClassInfo ClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            int classid = ClassInfo.ArticleClassID;
            //当前菜单id
            ViewData["ClassArticleId"] = ClassInfo.ArticleClassID;
            //获取当前菜单的名称
            ViewData["ClassArticleName"] = ClassInfo.ClassName;
            //三级菜单列表
            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classid);

            //判断是否有下级菜单 如果有肯定是当前肯定是二级

            if (ArticleClass.HaveChild(classid))
            {
                //当前是二级菜单，有三级菜单                
                //获取上级菜单的名称和id
                ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                if (Parentinfo != null)
                {
                    ClassArticleId1 = Parentinfo.ArticleClassID;
                    ClassArticleIdName1 = Parentinfo.ClassName;
                    ClassArticleId2 = classid;
                    ClassArticleIdName2 = ClassInfo.ClassName;
                    HaveChild3 = "display:none";

                }
            }
            else
            {
                ParentArticleClassID = Article.GetNameByArticleClassID2(classid);
                if (ParentArticleClassID > 0)
                {
                    ParentArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
                    if (ParentArticleClassID > 0)
                    {
                        //没有子菜单，但是有2个上级菜单，说明当前是3级菜单

                        ClassArticleId3 = classid;
                        ClassArticleIdName3 = ClassInfo.ClassName;
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                        ClassArticleId2 = Parentinfo.ArticleClassID;
                        ClassArticleIdName2 = Parentinfo.ClassName;
                        ClassArticleId1 = ParentArticleClassID;
                        ClassArticleIdName1 = ArticleClass.GetModelById(ParentArticleClassID).ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "";
                        //三级菜单列表
                        list = Article.GetArtClaIdByParentArticleClassIDs(ClassInfo.ParentArticleClassID);
                    }
                    else
                    {
                        //没有子菜单，但是1个上级菜单，说明当前是二级菜单
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                        ClassArticleId1 = Parentinfo.ArticleClassID;
                        ClassArticleIdName1 = Parentinfo.ClassName;
                        ClassArticleId2 = classid;
                        ClassArticleIdName2 = ClassInfo.ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "display:none";

                    }
                }
                else
                {
                    //没有子菜单，没有父级菜单，说明是1级菜单
                    ClassArticleId1 = classid;
                    ClassArticleIdName1 = ClassInfo.ClassName;
                    ClassArticleId2 = 0;
                    ClassArticleIdName2 = "";
                    HaveChild2 = "display:none";
                    HaveChild3 = "display:none";

                }
            }
            ViewData["list"] = list;
            ViewData["HaveChild2"] = HaveChild2;
            ViewData["HaveChild3"] = HaveChild3;
            ViewData["ClassArticleId1"] = ClassArticleId1;
            ViewData["ClassArticleId2"] = ClassArticleId2;
            ViewData["ClassArticleId3"] = ClassArticleId3;
            ViewData["ClassArticleIdName1"] = ClassArticleIdName1;
            ViewData["ClassArticleIdName2"] = ClassArticleIdName2;
            ViewData["ClassArticleIdName3"] = ClassArticleIdName3;

            //判断文章是否是微视频
            //info.DisplayType = 5; //微视频布局测试
            if (info.DisplayType.Equals(4))
            {
                ViewData["actlcle"] = "display:none";
                ViewData["video"] = "";
                //ViewData["videoUrl"] = "http://img.ksbbs.com/asset/Mon_1703/05cacb4e02f9d9e.mp4";
                //ViewData["videoUrl"] = "http://zzbz.net:8080/upload/article/video/source/" + info.MicroVideo;
                //ViewData["ckPlayer"] = "http://localhost:24809/Content/pcWeb/js/ckplayer/ckplayer.swf";
                ViewData["videoUrl"] = "/upload/article/video/source/" + info.MicroVideo;
                ViewData["ckPlayer"] = "/Content/pcWeb/js/ckplayer/ckplayer.swf";
                ViewData["ArticleImg"] = "display:none";
            }
            else
            {
                ViewData["actlcle"] = "";
                ViewData["video"] = "display:none";
                ViewData["videoUrl"] = "";
                ViewData["ArticleImg"] = "display:none";
            }
            if (articleClassInfo.ClassType.Equals(3))
            {
                ViewData["actlcle"] = "";
                ViewData["video"] = "display:none";
                ViewData["ArticleImg"] = "";
            }
            //外部链接转向
            if (info.DisplayType.Equals(1))
                return Redirect(info.Url);
            ArticleModel model = new ArticleModel
            {
                ArticleInfo = info,
                ArticleClassInfo = ArticleClass.GetModelById(info.ArticleClassID),
                ClssPath = ArticleClass.GetArticleClassPath(info.ArticleClassID)
            };
            //if (model.ArticleInfo.DisplayType.Equals(2))
            //    return View("details.info." + model.ArticleInfo.ArticleID, model);
            //else if (model.ArticleClassInfo.ContentView.Length > 0)
            //    return View("details." + model.ArticleClassInfo.ContentView, model);
            //else
            //访问量加一
            ArticleInfo info2 = Article.GetModelByArticleID(id);
            int Hits = Convert.ToInt32(info2.Hits) + 1;
            info2.Hits = Hits;
            Article.Update(info2);
            return View(model);

        }
        [HttpPost]
        public JsonResult JsonPraise()
        {
            string num = HttpContext.Request.Form["num"];
            string ArticleID = HttpContext.Request.Form["ArticleID"];
            ArticleInfo info2 = Article.GetModelByArticleID(Convert.ToInt32(ArticleID));
            int Praise = Convert.ToInt32(num);
            info2.Praise = Praise;
            Article.Update(info2);
            return json;
        }
        //带独有的的视频列表页
        public ActionResult ListVideo2()
        {
            int classid = GetRouteInt("ClassId");
            if (classid == 0)
                classid = WebHelper.GetQueryInt("ClassId");
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");
            //声明1级、2级、3级菜单信息
            int ClassArticleId1 = 0;
            string ClassArticleIdName1 = "";
            int ClassArticleId2 = 0;
            string ClassArticleIdName2 = "";
            int ClassArticleId3 = 0;
            string ClassArticleIdName3 = "";
            //父级菜单id
            int ParentArticleClassID = 0;
            //二级菜单显示隐藏
            string HaveChild2 = "";
            //三级菜单
            string HaveChild3 = "";
            ArticleClassInfo info = ArticleClass.GetModelById(classid);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");
            //三级菜单列表
            DataTable list = Article.GetArtClaIdByParentArticleClassIDs(classid);

            //判断是否有下级菜单 如果有肯定是当前肯定是二级

            if (ArticleClass.HaveChild(classid))
            {
                //当前是二级菜单，有三级菜单                
                //获取上级菜单的名称和id
                ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                if (Parentinfo != null)
                {
                    ClassArticleId1 = Parentinfo.ArticleClassID;
                    ClassArticleIdName1 = Parentinfo.ClassName;
                    ClassArticleId2 = classid;
                    ClassArticleIdName2 = info.ClassName;
                    HaveChild3 = "";
                    ArticleClassInfo info2 = ArticleClass.AdminGetArticleClassTreeList(classid, true)[0];
                    classid = info2.ArticleClassID;
                    ClassArticleId3 = info2.ArticleClassID;
                    ClassArticleIdName3 = info2.ClassName;

                }
            }
            else
            {
                ParentArticleClassID = Article.GetNameByArticleClassID2(classid);
                if (ParentArticleClassID > 0)
                {
                    ParentArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
                    if (ParentArticleClassID > 0)
                    {
                        //没有子菜单，但是有2个上级菜单，说明当前是3级菜单

                        ClassArticleId3 = classid;
                        ClassArticleIdName3 = info.ClassName;
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId2 = Parentinfo.ArticleClassID;
                        ClassArticleIdName2 = Parentinfo.ClassName;
                        ClassArticleId1 = ParentArticleClassID;
                        ClassArticleIdName1 = ArticleClass.GetModelById(ParentArticleClassID).ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "";
                        //三级菜单列表
                        list = Article.GetArtClaIdByParentArticleClassIDs(info.ParentArticleClassID);
                    }
                    else
                    {
                        //没有子菜单，但是1个上级菜单，说明当前是二级菜单
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(info.ParentArticleClassID);
                        ClassArticleId1 = Parentinfo.ArticleClassID;
                        ClassArticleIdName1 = Parentinfo.ClassName;
                        ClassArticleId2 = classid;
                        ClassArticleIdName2 = info.ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "display:none";

                    }
                }
                else
                {
                    //没有子菜单，没有父级菜单，说明是1级菜单
                    ClassArticleId1 = classid;
                    ClassArticleIdName1 = info.ClassName;
                    ClassArticleId2 = 0;
                    ClassArticleIdName2 = "";
                    HaveChild2 = "display:none";
                    HaveChild3 = "display:none";

                }
            }
            ViewData["list"] = list;
            ViewData["HaveChild2"] = HaveChild2;
            ViewData["HaveChild3"] = HaveChild3;
            ViewData["ClassArticleId1"] = ClassArticleId1;
            ViewData["ClassArticleId2"] = ClassArticleId2;
            ViewData["ClassArticleId3"] = ClassArticleId3;
            ViewData["ClassArticleIdName0"] = ClassArticleIdName1;
            ViewData["ClassArticleIdName2"] = ClassArticleIdName2;
            ViewData["ClassArticleIdName3"] = ClassArticleIdName3;
            if (info.ClassType.Equals(3))
            {
                ViewData["video"] = "display:none";
                ViewData["content-video-meng"] = "";
            }
            else
            {
                ViewData["content-video-meng"] = "content-video-meng";
                ViewData["video"] = "";
            }
            string condition = Article.GetArticleListCondition(classid, "");
            string sort = Article.GetArticleListSort("", "");
            PageModel pageModel = new PageModel(8, page, Article.GetArticleCount(condition));
            ViewData["ClassArticleIdName1"] = info.ClassName;
            ArticleListModel model = new ArticleListModel
            {
                ArticleClassID = classid,
                ArticleClassInfo = info,
                ClssPath = ArticleClass.GetArticleClassPath(classid),
                ArticleList = Article.GetArticleList(pageModel.PageSize, pageModel.PageNumber, condition, sort),
                PageModel = pageModel
            };
            //if (info.ListView.Length > 0)
            //    return View("List." + info.ListView, model);
            //else
            return View(model);
        }
        //单独的视频播放页
        public ActionResult videoShows(string ClassArticleId)
        {
            int id = GetRouteInt("id");
            if (id == 0)
                id = WebHelper.GetQueryInt("id");
            //新闻列表
            string ClassArticleIds = "";
            if (ClassArticleId == null)
            {
                ClassArticleIds = "";
            }
            else
            {
                ClassArticleIds = ClassArticleId;
            }
            ViewData["ClassArticleIds"] = ClassArticleIds;
            ArticleInfo info = Article.GetModelByArticleID(id);
            ArticleClassInfo articleClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            if (info == null)
                return PromptView("/", "您访问的页面不存在");

            if (info.IsShow == 0)
                return PromptView("/", "您访问的页面不存在");
            //声明1级、2级、3级菜单信息
            int ClassArticleId1 = 0;
            string ClassArticleIdName1 = "";
            int ClassArticleId2 = 0;
            string ClassArticleIdName2 = "";
            int ClassArticleId3 = 0;
            string ClassArticleIdName3 = "";
            //父级菜单id
            int ParentArticleClassID = 0;
            //二级菜单显示隐藏
            string HaveChild2 = "";
            //三级菜单
            string HaveChild3 = "";

            ArticleClassInfo ClassInfo = ArticleClass.GetModelById(info.ArticleClassID);
            int classid = ClassInfo.ArticleClassID;
            //当前菜单id
            ViewData["ClassArticleId"] = ClassInfo.ArticleClassID;
            //获取当前菜单的名称
            ViewData["ClassArticleName"] = ClassInfo.ClassName;
            //三级菜单列表
            DataTable list1 = Article.GetArtClaIdByParentArticleClassIDs(classid);

            //判断是否有下级菜单 如果有肯定是当前肯定是二级

            if (ArticleClass.HaveChild(classid))
            {
                //当前是二级菜单，有三级菜单                
                //获取上级菜单的名称和id
                ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                if (Parentinfo != null)
                {
                    ClassArticleId1 = Parentinfo.ArticleClassID;
                    ClassArticleIdName1 = Parentinfo.ClassName;
                    ClassArticleId2 = classid;
                    ClassArticleIdName2 = ClassInfo.ClassName;
                    HaveChild3 = "display:none";

                }
            }
            else
            {
                ParentArticleClassID = Article.GetNameByArticleClassID2(classid);
                if (ParentArticleClassID > 0)
                {
                    ParentArticleClassID = Article.GetNameByArticleClassID2(ParentArticleClassID);
                    if (ParentArticleClassID > 0)
                    {
                        //没有子菜单，但是有2个上级菜单，说明当前是3级菜单

                        ClassArticleId3 = classid;
                        ClassArticleIdName3 = ClassInfo.ClassName;
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                        ClassArticleId2 = Parentinfo.ArticleClassID;
                        ClassArticleIdName2 = Parentinfo.ClassName;
                        ClassArticleId1 = ParentArticleClassID;
                        ClassArticleIdName1 = ArticleClass.GetModelById(ParentArticleClassID).ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "";
                        //三级菜单列表
                        list1 = Article.GetArtClaIdByParentArticleClassIDs(ClassInfo.ParentArticleClassID);
                    }
                    else
                    {
                        //没有子菜单，但是1个上级菜单，说明当前是二级菜单
                        //获取上级菜单的名称和id
                        ArticleClassInfo Parentinfo = ArticleClass.GetModelById(ClassInfo.ParentArticleClassID);
                        ClassArticleId1 = Parentinfo.ArticleClassID;
                        ClassArticleIdName1 = Parentinfo.ClassName;
                        ClassArticleId2 = classid;
                        ClassArticleIdName2 = ClassInfo.ClassName;
                        HaveChild2 = "";
                        HaveChild3 = "display:none";

                    }
                }
                else
                {
                    //没有子菜单，没有父级菜单，说明是1级菜单
                    ClassArticleId1 = classid;
                    ClassArticleIdName1 = ClassInfo.ClassName;
                    ClassArticleId2 = 0;
                    ClassArticleIdName2 = "";
                    HaveChild2 = "display:none";
                    HaveChild3 = "display:none";

                }
            }
            ViewData["list"] = list1;
            ViewData["HaveChild2"] = HaveChild2;
            ViewData["HaveChild3"] = HaveChild3;
            ViewData["ClassArticleId1"] = ClassArticleId1;
            ViewData["ClassArticleId2"] = ClassArticleId2;
            ViewData["ClassArticleId3"] = ClassArticleId3;
            ViewData["ClassArticleIdName1"] = ClassArticleIdName1;
            ViewData["ClassArticleIdName2"] = ClassArticleIdName2;
            ViewData["ClassArticleIdName3"] = ClassArticleIdName3;

            //判断文章是否是微视频
            //info.DisplayType = 5; //微视频布局测试
            if (info.DisplayType.Equals(4))
            {
                ViewData["actlcle"] = "display:none";
                ViewData["video"] = "";
                //ViewData["videoUrl"] = "http://img.ksbbs.com/asset/Mon_1703/05cacb4e02f9d9e.mp4";
                //ViewData["videoUrl"] = "http://zzbz.net:8080/upload/article/video/source/" + info.MicroVideo;
                //ViewData["ckPlayer"] = "http://localhost:24809/Content/pcWeb/js/ckplayer/ckplayer.swf";
                ViewData["videoUrl"] = "/upload/article/video/source/" + info.MicroVideo;
                ViewData["ckPlayer"] = "/Content/pcWeb/js/ckplayer/ckplayer.swf";
                ViewData["ArticleImg"] = "display:none";
            }
            else
            {
                ViewData["actlcle"] = "";
                ViewData["video"] = "display:none";
                ViewData["videoUrl"] = "";
                ViewData["ArticleImg"] = "display:none";
            }
            if (articleClassInfo.ClassType.Equals(3))
            {
                ViewData["actlcle"] = "";
                ViewData["video"] = "display:none";
                ViewData["ArticleImg"] = "";
            }
            //外部链接转向
            if (info.DisplayType.Equals(1))
                return Redirect(info.Url);
            ArticleModel model = new ArticleModel
            {
                ArticleInfo = info,
                ArticleClassInfo = ArticleClass.GetModelById(info.ArticleClassID),
                ClssPath = ArticleClass.GetArticleClassPath(info.ArticleClassID)
            };
            //视频列表
            ViewData["ListVideoArticleClass"] = "";
            DataTable ListVideo = Article.GetArtClaIdByParentArticleClassIDs(304);
            if (ListVideo != null && ListVideo.Rows.Count > 0)
            {
                string ListVideoArticleClass = "";
                foreach (DataRow dr in ListVideo.Rows)
                {
                    ListVideoArticleClass += dr["ArticleClassID"].ToString() + ",";
                }
                ListVideoArticleClass = ListVideoArticleClass.Substring(0, ListVideoArticleClass.Length - 1);
                ViewData["ListVideoArticleClass"] = ListVideoArticleClass;
            }

            //if (model.ArticleInfo.DisplayType.Equals(2))
            //    return View("details.info." + model.ArticleInfo.ArticleID, model);
            //else if (model.ArticleClassInfo.ContentView.Length > 0)
            //    return View("details." + model.ArticleClassInfo.ContentView, model);
            //else
            //访问量加一
            ArticleInfo info2 = Article.GetModelByArticleID(id);
            int Hits = Convert.ToInt32(info2.Hits) + 1;
            info2.Hits = Hits;
            Article.Update(info2);
            return View(model);

        }
    }

}
