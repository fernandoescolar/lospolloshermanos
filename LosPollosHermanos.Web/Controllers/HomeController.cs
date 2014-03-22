using System.Collections.Generic;
using System.Web.Mvc;
using LosPollosHermanos.Web.Models;

namespace LosPollosHermanos.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Products()
        {
            var model = new List<ProductModel>
                        {
                            new ProductModel{Id = 1, Name = "Pollos Strips", Price = 7.95m},
                            new ProductModel{Id = 2, Name = "Chips", Price = 2.95m},
                            new ProductModel{Id = 3, Name = "Crispy Pollos", Price = 8.90m},
                            new ProductModel{Id = 4, Name = "Polos Wings", Price = 6.80m},
                        };

            return Json(model);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderLineModel[] lines)
        {
            

            return Json(true);
        }
    }
}