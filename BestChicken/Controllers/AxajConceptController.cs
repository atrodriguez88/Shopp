using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace BestChicken.Controllers
{
    public class AxajConceptController : Controller
    {
        // GET: AxajConcept
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult JsonFunct(int n)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult
            {
                Data = fun(n)
            };
            return result;
        }

        private double fun(int n)
        {
            return (n * 5.6);
        }
    }
}