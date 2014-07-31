using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ActionResult Index(PostsModel posts)
        {
            if (!string.IsNullOrEmpty(posts.PostTitle) && !string.IsNullOrEmpty(posts.PostDetails.PostText))
            {
                PostsDto postsDto = BuildPostDto(posts);
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

        private PostsDto BuildPostDto(PostsModel posts)
        {
            var postsDto = new PostsDto
            {
                PostId = Guid.NewGuid(),
                PostTitle = posts.PostTitle,
                PostDateTime = DateTime.Now,
                PostDetailsDto = BuildPostDetailsDto(posts.PostDetails),
                PostDeleted = false,
                UserId = posts.UserId
            };

            postsDto.PostDetailsDto.PostId = postsDto.PostId;
            return postsDto;
        }

        private PostDetailsDto BuildPostDetailsDto(PostDetails postDetails)
        {
            var postDetailsDto = new PostDetailsDto
            {
                PostDetailId = Guid.NewGuid(),
                PostText = postDetails.PostText,
                PostImage = postDetails.PostImage
            };

            return postDetailsDto;
        }

        #endregion

    }
}