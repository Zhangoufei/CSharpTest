using System;
using System.Dynamic;
using CyPhone.Common;
using CyPhone.Common.UI;
using CyPhone.DAL;
using CyPhone.Model;
using System.Collections.Generic;

namespace CyPhone.BLL
{
    public partial class ArticleBll
    {
        private readonly ArticleDal dal = new ArticleDal();
        public ArticleBll()
        { }
        /// <summary>
        /// 根据关键词获取新闻列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public  List<bs_Article> GetArticleListByKeyWords(string keywords, int pageNum, int pageSize)
        {
            return dal.GetArticleListByKeyWords(keywords,pageNum,pageSize);
        }
        /// <summary>
        /// 获取数据总数
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public  int GetArticleRecordCount(string keywords)
        {
            return dal.GetArticleRecordCount(keywords);
        }
    }
}
