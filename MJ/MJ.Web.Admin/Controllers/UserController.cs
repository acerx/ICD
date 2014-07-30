using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MJ.Web.Admin.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        // POST USING AJAX
        public ActionResult Register(string username, string password)
        {
            return Json(new { success = true });
        }

	}
}