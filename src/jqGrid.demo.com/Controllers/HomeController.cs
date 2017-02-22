using System.Collections.Generic;
using System.Web.Mvc;

namespace jqGrid.demo.com.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var list = new List<ReturnViewModel>();
            for (var i = 0; i < 10; i++)
            {
                list.Add(new ReturnViewModel() { Id = i });
            }
            var result = new
            {
                PageIndex = 1,
                TotalPage = 1,
                Rows = list.ToArray(),
                TotalRows = list.Count
            };
            return Json(result);
        }
    }

    public class ReturnViewModel
    {
        public int Id { get; set; }
    }
}