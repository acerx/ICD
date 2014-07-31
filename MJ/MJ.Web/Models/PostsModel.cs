using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MJ.Common.DTO;
using PagedList;

namespace MJ.Web.Models
{
    public class PostsModel
    {

        public Guid PostId { get; set; }

        public string PostTitle { get; set; }

        public DateTime PostDateTime { get; set; }

        public DateTime PostDeleted { get; set; }

        public Guid UserId { get; set; }

        public string Username { get; set; }

        public PostDetails PostDetails { get; set; }

    }

    public class PostDetails
    {

        public Guid PostDetailId { get; set; }

        public Guid PostId { get; set; }

        public string PostText { get; set; }

        public string PostImage { get; set; }
    }


    public class Post
    {
        public int? Page { get; set; }

        public IPagedList<PostsModel> PostList { get; set; }
    }
}