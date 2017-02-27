using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace jqGrid.demo.com.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
            DataSource = new List<ReturnViewModel>();
            for (var i = 0; i < 10001; i++)
            {
                DataSource.Add(new ReturnViewModel() { Id = i + 1, Name = "名称" + i + 1, ParentId = i > 20 ? i % 20 + 1 : -1 });
            }
        }

        public List<ReturnViewModel> DataSource { get; set; }

        protected virtual void BindParam(GridParamModel model)
        {
            if (model.PageIndex < 1) model.PageIndex = 1;
            if (model.PageSize < 1) model.PageSize = 20;
        }

        // GET: Home
        public ActionResult Index(HomeSearchGridParamModel model)
        {
            return View(model);
        }

        public ActionResult List(HomeSearchGridParamModel model)
        {
            var list = DataSource.Where(p => p.ParentId == model.ParentId).ToList();
            var rows = list.Skip((model.PageIndex - 1) * model.PageSize).Take(model.PageSize).ToArray();
            var result = new GridReturnModel
            {
                PageIndex = model.PageIndex,
                TotalPage = (list.Count + model.PageSize - 1) / model.PageSize,
                Rows = rows,
                TotalRows = list.Count
            };
            return Json(result);
        }
        /// <summary>
        /// 子父级测试
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SubIndex(HomeSearchGridParamModel model)
        {
            return View(model); 
        }


    }

    public class ReturnViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }
    }

    /// <summary>
    /// grid参数模型
    /// </summary>
    public class GridParamModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GridReturnModel
    {
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public dynamic Rows { get; set; }
        public int TotalRows { get; set; }
    }


    public class HomeSearchGridParamModel : GridParamModel
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }
    }
}