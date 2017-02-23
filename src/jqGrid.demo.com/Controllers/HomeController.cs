using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace jqGrid.demo.com.Controllers
{
    public class HomeController : Controller
    {
        protected virtual void BindParam(GridParamModel model)
        {
            if (model.PageIndex < 1) model.PageIndex = 1;
            if (model.PageSize < 1) model.PageSize = 20;
        }

        // GET: Home
        public ActionResult Index(GridParamModel model)
        {
            return View(model);
        }

        public ActionResult List(GridParamModel model)
        {
            var list = new List<ReturnViewModel>();
            for (var i = 0; i < 101; i++)
            {
                list.Add(new ReturnViewModel() { Id = i, Name = "名称" + i + 1 });
            }

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
    }

    public class ReturnViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
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
}