using System.Web.Mvc;
using BrownSugarBakes.TTR.DATA.EF;
using BrownSugarBakes.UI.MVC.Models.Home;
using BrownSugarBakes.UI.MVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System;
using BrownSugarBakes.UI.MVC.Repository;

namespace BrownSugarBakes.UI.MVC.Controllers
{

    public class HomeController : Controller
    {
        dbBrownSugarBakesEntities ctx = new dbBrownSugarBakesEntities();
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int productId, string url)
        {
            if (Session["cart"] != null)
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });

                        }
                        break;
                    }
                }
                Session["cart"] = cart;
            }
            return Redirect(url);
        }
        public ActionResult AddToCart(int productId, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["cart"] = cart;
            }


            return Redirect(url);
        }

        public ActionResult RemoveFromCart(int productId, string url)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);

                    break;
                }

            }

            Session["cart"] = cart;
            return Redirect("CheckoutDetails");

        }
        public ActionResult ShoppingCart()
        {

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult ShippingInfo()
        {


            return View();
        }

        public ActionResult ShippingInfoAdd()
        {


            return View();
        }

        [HttpPost]
        public ActionResult ShippingInfoAdd(Tbl_ShippingDetails sc)
        {
                _unitOfWork.GetRepositoryInstance<Tbl_ShippingDetails>().Add(sc);
                Session["shipCart"] = sc;
       
            return RedirectToAction("CheckoutDetails");
        }

    }
}
