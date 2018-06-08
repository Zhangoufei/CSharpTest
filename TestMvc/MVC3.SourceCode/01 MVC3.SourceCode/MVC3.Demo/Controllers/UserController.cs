
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2013-2-24
 *
 * 描  述：用户控制器
 *
 */

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using MVC3.Demo.App_Code;

namespace MVC3.Demo.Controllers
{
    public class UserController : Controller
    {
        ////系统自动生成，可以删除
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //登陆控制器
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]//登陆控制器
        public ActionResult Login(Models.LoginModel loginModel)
        {
            if (loginModel.UserName == "张三" && loginModel.Password == "123456")
                Response.Write("正确！");
            else
                Response.Write("不正确！");
            return View();
        }

        //RazorDemo控制器
        public ActionResult RazorDemo()
        {
            return View();
        }

        //HtmlHelper
        public ActionResult HtmlHelper()
        {
            //实体
            Models.UserModel user = new Models.UserModel();
            user.UserName = "用户名";
            user.Password = "123456";
            user.Sex = 1;

            //下拉列表等的值
            List<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Value = "1", Text = "列表项一" });
            selectList.Add(new SelectListItem { Value = "2", Text = "列表项二" });
            //保存到ViewBag中
            ViewBag.SelectData = new SelectList(selectList);

            return View(user);
        }

        //LayoutDemo_01
        public ActionResult LayoutDemo_01()
        {
            ViewBag.Title = "布局页一";
            return View();
        }

        //LayoutDemo_02
        public ActionResult LayoutDemo_02()
        {
            ViewBag.Title = "布局页二";
            return View();
        }

        //LayoutDemo_03
        public ActionResult LayoutDemo_03()
        {
            ViewBag.Title = "布局页三";
            return View();
        }

        //UserDetail
        public ActionResult UserDetail()
        {
            Models.UserModel userModel = new Models.UserModel();
            userModel.UserName = "用户名";
            userModel.Password = "密码";
            userModel.Sex = 0;
            userModel.Age = 30;
            userModel.Height = 170;
            userModel.Weight = 70;
            userModel.Graduated = "毕业院校";
            return View(userModel);
        }

        //UserEdit
        public ActionResult UserEdit()
        {
            Models.UserModel userModel = new Models.UserModel();
            userModel.UserName = "用户名";
            userModel.Age = 10;
            userModel.Sex = 0;
            //其他....
            return View(userModel);
        }

        [HttpPost]//UserEdit
        public ActionResult UserEdit(Models.UserModel userModel)
        {
            Response.Write(userModel.UserName);
            Response.Write("<br />");
            Response.Write(userModel.Password);
            Response.Write("<br />");
            Response.Write(userModel.Sex);
            Response.Write("<br />");
            Response.Write(userModel.Age);
            Response.Write("<br />");
            Response.Write(userModel.Height);
            Response.Write("<br />");
            Response.Write(userModel.Weight);
            Response.Write("<br />");
            Response.Write(userModel.Graduated);
            Response.Write("<br />");
            return View();
        }

        //UserEdit
        public ActionResult UserEdit_01()
        {
            return View();
        }

        [HttpPost]//UserEdit
        public ActionResult UserEdit_01(FormCollection form)
        {
            Response.Write(form["UserName"]);
            Response.Write("<br />");
            Response.Write(form[1]);
            Response.Write("<br />");
            return View();
        }

        [LogActionFilter(LogMessage = "Validation方法")]
        public ActionResult Validation()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validation(Models.ValidationModel model)
        {
            if (ModelState.IsValid)
            {
                var inputNumber = model.InputNumber;
            }
            return View();
        }
    }
}