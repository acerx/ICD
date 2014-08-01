using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MJ.Common.DTO;
using MJ.Services;
using MJ.Services.IServices;
using PagedList;
using MJ.Web.Models;

namespace MJ.Web.Controllers
{
    public class BlogController : Controller
    {
        #region SERVICES

        private IReadPostService _reaadPostService;

        public IReadPostService ReadPostService
        {
            get { return _reaadPostService ?? (_reaadPostService = new ReadPostService()); }
        }

        private IUserService _userService;
        public IUserService UserService
        {
            get { return _userService ?? (_userService = new UserService()); }
        }

        #endregion

        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blogs(Post post)
        {
            IEnumerable<PostsDto> posts = ReadPostService.GetAllPosts();

            var model = new Post();

            var pageIndex = post.Page ?? 1;

            model.PostList = FillInPostList(posts, pageIndex, 3);

            return PartialView(model);
        }

        #region PRIVATE

        private IPagedList<PostsModel> FillInPostList(IEnumerable<PostsDto> posts, int pageIndex, int p)
        {
            var postList = new List<PostsModel>();

            foreach (var item in posts)
            {
                var post = new PostsModel();
                string un = UserService.GetUsername(item.UserId);
                string username = un.Substring(0, un.IndexOf('@'));


                post.PostTitle = item.PostTitle;
                post.PostText = item.PostDetailsDto.PostText;
                post.PostImage = string.IsNullOrEmpty(item.PostDetailsDto.PostImage)
                    ? ""
                    : item.PostDetailsDto.PostImage;
                post.PostDateTime = item.PostDateTime.ToString("ddd MMM. dd, yyyy - hh:mm tt");
                post.Username = username;
                postList.Add(post);
            }

            return postList.ToPagedList(pageIndex, p);
        }

        #endregion
	}
}