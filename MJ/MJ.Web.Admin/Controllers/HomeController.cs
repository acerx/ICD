using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJ.Web.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.result = TempData["Message"] as string;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        // POST USING AJAX
        public ActionResult Register(string username, string password)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                var result = "Failed";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                TempData["Message"] = "Successfully Created new account. Please login.";
                var result = "Success";
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true });
        }

    }
}