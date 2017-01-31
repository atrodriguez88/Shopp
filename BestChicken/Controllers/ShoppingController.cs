using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BestChicken.Models;
using BestChicken.ViewModels;
using Microsoft.Ajax.Utilities;

namespace BestChicken.Controllers
{
    public class ShoppingController : Controller
    {
        BestChickenContext db = new BestChickenContext();
        
        // GET: Shopping
        public ActionResult NewShopping()
        {
            var shoppingView =  new ShoppingView();
            shoppingView.Supplier =new Supplier();
            shoppingView.Products = new List<ProductShopping>();
            Session["shoppingView"] = shoppingView;

            var list = db.Suppliers.ToList();
            list.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
            list = list.OrderBy(costumer => costumer.Name).ToList();
            ViewBag.SupplierId = new SelectList(list, "SupplierId", "Name");

            return View(shoppingView);
        }

        [HttpPost]
        public ActionResult NewShopping(ShoppingView shoppingView)
        {
            shoppingView = Session["shoppingView"] as ShoppingView;
            int SupplierId = Convert.ToInt32(Request["SupplierId"]);
            if (SupplierId == 0)
            {
                var list = db.Suppliers.ToList();
                list.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
                list = list.OrderBy(costumer => costumer.Name).ToList();
                ViewBag.SupplierId = new SelectList(list, "SupplierId", "Name");
                ViewBag.Error = "Select a Supplier";
                return View(shoppingView);
            }
            else
            {
                Supplier supplier = db.Suppliers.Find(SupplierId);
                if (supplier != null)
                {
                    if (shoppingView.Products.Count == 0)
                    {
                        var listN = db.Suppliers.ToList();
                        listN.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
                        listN = listN.OrderBy(costumer => costumer.Name).ToList();
                        ViewBag.SupplierId = new SelectList(listN, "SupplierId", "Name");
                        ViewBag.Error = "Select a Supplier";
                        return View(shoppingView);
                    }

                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var order = new Shopping()
                            {
                                SupplierId = SupplierId,
                                ShoppingDate = DateTime.Now,
                                ShoppingState = ShoppingState.Create,
                                
                                
                            };
                            db.Shoppings.Add(order);
                            db.SaveChanges();

                            var shoppId = db.Shoppings.ToList().Select(order1 => order1.ShoppingId).Max();

                            foreach (var item in shoppingView.Products)
                            {
                                var prod = new ShoppingDetail()
                                {
                                    Count = item.Count,
                                    ProductId = item.ProductId,
                                    Valor = item.Valor,
                                    ShoppingId = shoppId,
                                    Description = item.Description,
                                };
                                db.ShoppingDetails.Add(prod);
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
                            return View(shoppingView);
                        }
                    }




                    shoppingView = new ShoppingView();
                    shoppingView.Supplier = new Supplier();
                    shoppingView.Products = new List<ProductShopping>();
                    Session["shoppingView"] = shoppingView;

                    var listB = db.Suppliers.ToList();
                    listB.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
                    listB = listB.OrderBy(costumer => costumer.Name).ToList();
                    ViewBag.SupplierId = new SelectList(listB, "SupplierId", "Name");
                    ViewBag.Error = "Select a Add";

                    return View(shoppingView);

                }
                var listR = db.Suppliers.ToList();
                listR.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
                listR = listR.OrderBy(costumer => costumer.Name).ToList();
                ViewBag.SupplierId = new SelectList(listR, "SupplierId", "Name");
                ViewBag.Error = "Select a Supplier";
                return View(shoppingView);
            }
        }

        // GET: Shopping/Details/5
        [NonAction]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Shopping/Create
        public ActionResult AddSupplier()
        {
            var list = db.Products.ToList();
            list.Add(new Product { ProductId = 0, ProductName = "[Select a Product]" });
            list = list.OrderBy(costumer => costumer.ProductName).ToList();

            ViewBag.ProductId = new SelectList(list, "ProductId", "ProductName");
            return View();
        }

        // POST: Shopping/Create
        [HttpPost]
        public ActionResult AddSupplier(ProductShopping productShopping)
        {
            bool flag = false;
            var list = db.Products.ToList();
            list.Add(new Product { ProductId = 0, ProductName = "[Select a Product]" });
            list = list.OrderBy(costumer => costumer.ProductName).ToList();
            ViewBag.ProductId = new SelectList(list, "ProductId", "ProductName");

            if (productShopping.ProductId == 0 || productShopping.Count == 0)
            {
                ViewBag.Error = "Error to create a product";
                return View(productShopping);
            }
            var shoppingView = Session["shoppingView"] as ShoppingView;
            foreach (ProductShopping product in shoppingView.Products)
            {
                if (productShopping.ProductId == product.ProductId)
                {
                    product.Count += productShopping.Count;
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                var Prod = list.Find(product => product.ProductId == productShopping.ProductId);
                productShopping.ProductName = Prod.ProductName;
                productShopping.Description = Prod.Description;
                productShopping.Precio = Prod.Precio;

                shoppingView.Products.Add(productShopping);
            }


            var listC = db.Suppliers.ToList();
            listC.Add(new Supplier { SupplierId = 0, Name = "[Select a Supplier]" });
            listC = listC.OrderBy(supplier => supplier.Name).ToList();
            ViewBag.SupplierId = new SelectList(listC, "SupplierId", "Name");

            return View("NewShopping", shoppingView);
        }

        // GET: Shopping/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Shopping/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("NewOrder");
            }
            catch
            {
                return View();
            }
        }

        // GET: Shopping/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Shopping/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("NewOrder");
            }
            catch
            {
                return View();
            }
        }
    }
}
