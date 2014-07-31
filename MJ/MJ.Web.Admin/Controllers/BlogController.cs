using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MJ.Web.Admin.Models;

namespace MJ.Web.Admin.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"] as string;
            return View();
        }

        // POST:
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PostsModel posts)
        {
            if (!string.IsNullOrEmpty(posts.PostTitle) && !string.IsNullOrEmpty(posts.PostDetails.PostText))
            {
                TempData["Message"] = "Successfully added new post.";
                return RedirectToAction("Index", "Blog");
            }

            return View();
        }
    }
}