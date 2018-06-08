
/*
 *
 * 创建人：李林峰
 * 
 * 时  间：2012-03-25
 *
 * 描  述：地址类的模型，通过GetResidetialItem方法，把地址根据Type类型添加到不同的SelectList中
 *
 */

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using Model = MVC3.DemoModel;

namespace MVC3.Demo_Whole
{
    public class AddressHelper
    {
        //单例
        private AddressHelper() { }
        private static AddressHelper Instance = new AddressHelper();
        public static AddressHelper GetInstance()
        {
            return Instance;
        }

        public SelectList ResidetialItem { get; private set; }
        public SelectList FloorNoItem { get; private set; }
        public SelectList UnitNoItem { get; private set; }
        public SelectList DoorplateNoItem { get; private set; }

        //获取小区列表
        public void GetResidetialItem(List<Model.Address> AddressItem)
        {
            List<SelectListItem> ResidetialItem = new List<SelectListItem>();
            List<SelectListItem> FloorNoItem = new List<SelectListItem>();
            List<SelectListItem> UnitNoItem = new List<SelectListItem>();
            List<SelectListItem> DoorplateNoItem = new List<SelectListItem>();
            foreach (Model.Address address in AddressItem)
            {
                if (address.Type == 1)
                {
                    ResidetialItem.Add(new SelectListItem { Value = address.AddressID.ToString(), Text = address.Name });
                }
                if (address.Type == 2)
                {
                    FloorNoItem.Add(new SelectListItem { Value = address.AddressID.ToString(), Text = address.Name });
                }
                if (address.Type == 3)
                {
                    UnitNoItem.Add(new SelectListItem { Value = address.AddressID.ToString(), Text = address.Name });
                }
                if (address.Type == 4)
                {
                    DoorplateNoItem.Add(new SelectListItem { Value = address.AddressID.ToString(), Text = address.Name });
                }
            }
            this.ResidetialItem = new SelectList(ResidetialItem.AsEnumerable(), "Value", "Text");
            this.FloorNoItem = new SelectList(FloorNoItem.AsEnumerable(), "Value", "Text");
            this.UnitNoItem = new SelectList(UnitNoItem.AsEnumerable(), "Value", "Text");
            this.DoorplateNoItem = new SelectList(DoorplateNoItem.AsEnumerable(), "Value", "Text");
        }
    }
}