using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BestChicken.Models;
using BestChicken.ViewModels;

namespace BestChicken.Controllers
{
    public class OrdersController : Controller
    {

        BestChickenContext db = new BestChickenContext();
        // GET: Orders
        public ActionResult NewOrder()
        {
            var orderview = new OrderView();
            orderview.Costumer = new Costumer();
            orderview.Products = new List<ProductOrder>();
            Session["orderview"] = orderview;

            var list = db.Costumers.ToList();
            list.Add(new Costumer {CostumerId = 0,FirstName= "[Select a Client]"});
            list = list.OrderBy(costumer => costumer.FirstName).ToList();
            ViewBag.CostumerId = new SelectList(list, "CostumerId", "FullName");

            return View(orderview);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(OrderView orderview)
        {
            orderview = Session["orderview"] as OrderView;

            int customerId = Convert.ToInt32(Request["CostumerId"]);
            if (customerId == 0)
            {
                var list = db.Costumers.ToList();
                list.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                list = list.OrderBy(costumer => costumer.FirstName).ToList();
                ViewBag.CostumerId = new SelectList(list, "CostumerId", "FullName");
                ViewBag.Error = "Select a Product";
                return View(orderview);
            }
            else
            {
                Costumer client = db.Costumers.Find(customerId);
                if (client != null)
                {
                    if (orderview.Products.Count == 0)
                    {
                        var listP = db.Costumers.ToList();
                        listP.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                        listP = listP.OrderBy(costumer => costumer.FirstName).ToList();
                        ViewBag.CostumerId = new SelectList(listP, "CostumerId", "FullName");
                        ViewBag.Error = "Select a Product";
                        return View(orderview);
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var order = new Order
                            {
                                CostumerId = customerId,
                                OrderDate = DateTime.Now,
                                OrderState = OrderState.Create,
                            };
                            db.Orders.Add(order);
                            db.SaveChanges();

                            var ordenId = db.Orders.ToList().Select(order1 => order1.OrderId).Max();
                           

                            foreach (var item in orderview.Products)
                            {
                                var inventarys = db.Inventories.ToList();
                                var invent = inventarys.Find(n=>n.Name == item.ProductName);
                                int produCount = invent.Quantity - item.Count;
                                inventarys.Find(n => n.Name == item.ProductName).Quantity = produCount;

                                var prod = new OrderDetail()
                                {
                                    Count = item.Count,
                                    ProductId = item.ProductId,
                                    Valor = item.Valor,
                                    OrderId = ordenId,
                                    Description = item.Description,
                                };
                                db.OrderDetails.Add(prod);
                            }
                            db.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception exception)
                        {
                            transaction.Rollback();
                            var listP = db.Costumers.ToList();
                            listP.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                            listP = listP.OrderBy(costumer => costumer.FirstName).ToList();
                            ViewBag.CostumerId = new SelectList(listP, "CostumerId", "FullName");
                            ViewBag.Error = "ERROR: " + exception.Message;
                            return View(orderview);
                        }
                    }

                   


                    orderview = new OrderView();
                    orderview.Costumer = new Costumer();
                    orderview.Products = new List<ProductOrder>();
                    Session["orderview"] = orderview;

                    var listR = db.Costumers.ToList();
                    listR.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                    listR = listR.OrderBy(costumer => costumer.FirstName).ToList();
                    ViewBag.CostumerId = new SelectList(listR, "CostumerId", "FullName");
                    ViewBag.Add = "Product Add";

                    

                    return View(orderview);

                }
                var list = db.Costumers.ToList();
                list.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                list = list.OrderBy(costumer => costumer.FirstName).ToList();
                ViewBag.CostumerId = new SelectList(list, "CostumerId", "FullName");
                ViewBag.Error = "Select a Client";
                return View(orderview);
            }
        }

        public ActionResult AddProduct()
        {
            var list = db.Products.ToList();
            list.Add(new Product {ProductId = 0,ProductName = "[Select a Product]" });
            list = list.OrderBy(costumer => costumer.ProductName).ToList();

            ViewBag.ProductId = new SelectList(list, "ProductId", "ProductName");
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(ProductOrder productOrder)
        {
            bool flag = false;
            var list = db.Products.ToList();
            list.Add(new Product { ProductId = 0, ProductName = "[Select a Product]" });
            list = list.OrderBy(costumer => costumer.ProductName).ToList();
            ViewBag.ProductId = new SelectList(list, "ProductId", "ProductName");

            if (productOrder.ProductId == 0 || productOrder.Count == 0)
            {
                ViewBag.Error = "Error to create a product";
                return View(productOrder);
            }
            var orderview = Session["orderview"] as OrderView;
            foreach (ProductOrder product in orderview.Products)
            {
                if (productOrder.ProductId == product.ProductId)
                {
                    product.Count += productOrder.Count;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                var Prod = list.Find(product => product.ProductId == productOrder.ProductId);
                productOrder.ProductName = Prod.ProductName;
                productOrder.Description = Prod.Description;
                productOrder.Precio = Prod.Precio;

                orderview.Products.Add(productOrder);
            }
           

            var listC = db.Costumers.ToList();
            listC.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
            listC = listC.OrderBy(costumer => costumer.FirstName).ToList();
            ViewBag.CostumerId = new SelectList(listC, "CostumerId", "FullName");

            return View("NewOrder", orderview);
        }

        public ActionResult Edit(int? id)
        {
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var orderview = Session["orderview"] as OrderView;
                if (orderview == null)
                {
                    return HttpNotFound();
                }
                var product = orderview.Products.Find(order => order.ProductId == id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ProductOrder productOrder = new ProductOrder()
                    {
                        ProductId = product.ProductId,
                        Count =  product.Count,
                        Precio = product.Precio,
                        ProductName = product.ProductName,
                        Description = product.Description
                    };
                    return View(productOrder);
                }
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductOrder productOrder)
        {
            var orderview = Session["orderview"] as OrderView;
            var prodOrder = orderview.Products.Find(order => order.ProductId == productOrder.ProductId);
            prodOrder.Count = productOrder.Count;

            var list = db.Costumers.ToList();
            list.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
            list = list.OrderBy(costumer => costumer.FirstName).ToList();
            ViewBag.CostumerId = new SelectList(list, "CostumerId", "FullName");
            return View("NewOrder", orderview);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                var orderview = Session["orderview"] as OrderView;
                if (orderview == null)
                {
                    return HttpNotFound();
                }
                
                if (!orderview.Products.Exists(order => order.ProductId == id))
                {
                    return HttpNotFound();
                }
                else
                {
                    orderview.Products.Remove(orderview.Products.Find(order => order.ProductId == id));

                    var list = db.Costumers.ToList();
                    list.Add(new Costumer { CostumerId = 0, FirstName = "[Select a Client]" });
                    list = list.OrderBy(costumer => costumer.FirstName).ToList();
                    ViewBag.CostumerId = new SelectList(list, "CostumerId", "FullName");

                    return View("NewOrder",orderview);
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}