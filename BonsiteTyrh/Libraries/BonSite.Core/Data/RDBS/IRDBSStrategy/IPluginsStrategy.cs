using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BonSite.Core
{
    public partial interface IRDBSStrategy
    {

        int CreateServiceEval(ServiceEvalInfo serviceEvalInfo);

        bool DeleteServiceEvalById(string idlist);

        bool UpdateServiceEval(ServiceEvalInfo model);

        IDataReader GetServiceEvalInfoById(int id);

        DataTable AdminGetServiceEvalList(int pageSize, int pageNumber, string condition, string sort);

        string AdminGetServiceEvalListCondition(string title);

        string AdminGetServiceEvalListSort(string sortColumn, string sortDirection);

        int AdminGetServiceEvalCount(string condition);



        int CreateProductFeedbacks(ProductFeedbacksInfo serviceEvalInfo);

        bool DeleteProductFeedbacksById(string idlist);

        bool UpdateProductFeedbacks(ProductFeedbacksInfo model);

        IDataReader GetProductFeedbacksInfoById(int id);

        DataTable AdminGetProductFeedbacksList(int pageSize, int pageNumber, string condition, string sort);

        string AdminGetProductFeedbacksListCondition(string title);

        string AdminGetProductFeedbacksListSort(string sortColumn, string sortDirection);

        int AdminGetProductFeedbacksCount(string condition);


        string GetArticleListConditionList(List<int> articleClassId, string title);


    }
}
