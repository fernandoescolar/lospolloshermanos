using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LosPollosHermanos.Infrastructure;
using LosPollosHermanos.ServiceContracts;
using LosPollosHermanos.Web.Infrastructure;
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

            var model = new List<ProductModel>();

            var service = new ProductsService();

            var products = service.GetAvailableProducts();

            model.AddRange(products.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }));
           
            return Json(model);
        }

        [HttpPost]
        public ActionResult AddOrder(OrderLineModel[] lines)
        {
            var dto = new OrderRequest
                      {
                          Client = Guid.NewGuid(),
                          Lines = lines.Select(o => new OrderRequestLine
                                                    {
                                                        ProductId = o.ProductId,
                                                        Quantity = o.Quantity
                                                    }).ToArray()
                      };

            using (var proxy = new Proxy<IOrdersService>("OrdersService"))
            {
                proxy.Call(s => s.SendOrder(dto));
            }

            return Json(true);
        }
    }
}