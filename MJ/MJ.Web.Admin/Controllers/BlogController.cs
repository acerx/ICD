using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MJ.Common.DTO;
using MJ.Services;
using MJ.Services.IServices;
using MJ.Web.Admin.Models;

namespace MJ.Web.Admin.Controllers
{
    public class BlogController : Controller
    {

        #region SERVICES

        private IPostService _postService;

        public IPostService PostService
        {
            get { return _postService ?? (_postService = new PostService()); }
        }

        #endregion

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
        public ActionResult Index(PostsModel posts, HttpPostedFileBase file)
        {
            //if (PostService.CheckPostTitle(posts.PostTitle))
            //{
            //    TempData["Message"] = "Duplicate title found in record. Please try again.";
            //    return RedirectToAction("Index", "Blog");
            //}

            if (!string.IsNullOrEmpty(posts.PostTitle) && !string.IsNullOrEmpty(posts.PostDetails.PostText))
            {
                PostsDto postsDto = BuildPostDto(posts, file);
                bool check = PostService.AddPost(postsDto);

                if (check)
                {
                    TempData["Message"] = "Successfully added new post.";
                    return RedirectToAction("Index", "Blog");
                }

            }

            return View();
        }

        #region PRIVATES

        private PostsDto BuildPostDto(PostsModel posts, HttpPostedFileBase file)
        {
            var postsDto = new PostsDto
            {
                PostId = Guid.NewGuid(),
                PostTitle = posts.PostTitle,
                PostDateTime = DateTime.Now,
                PostDetailsDto = BuildPostDetailsDto(posts.PostDetails, file),
                PostDeleted = false,
                UserId = posts.UserId
            };

            postsDto.PostDetailsDto.PostId = postsDto.PostId;
            return postsDto;
        }

        private PostDetailsDto BuildPostDetailsDto(PostDetails postDetails, HttpPostedFileBase file)
        {
            PostDetailsDto postDetailsDto;

            if (file != null && file.ContentLength > 0)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                var fileName = Guid.NewGuid() + fileExtension;
                var path = Path.Combine(Server.MapPath("~/PostImages"), fileName);

                var photo = new WebImage(file.InputStream);

                if (photo != null)
                {
                    var userPicture = photo.Clone().Crop(1, 1, 1, 1);
                    userPicture.Save(path);
                }

                postDetailsDto = new PostDetailsDto
                {
                    PostDetailId = Guid.NewGuid(),
                    PostText = postDetails.PostText,
                    PostImage = fileName.ToString(CultureInfo.InvariantCulture)
                };

                return postDetailsDto;
            }
            else
            {
                postDetailsDto = new PostDetailsDto
                {
                    PostDetailId = Guid.NewGuid(),
                    PostText = postDetails.PostText,
                    PostImage = string.Empty
                };

                return postDetailsDto;
            }
            
        }

        #endregion

    }
}