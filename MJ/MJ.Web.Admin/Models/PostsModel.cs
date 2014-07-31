using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MJ.Web.Admin.Models
{
    public class PostsModel
    {

        public Guid PostId { get; set; }

        [Required]
        public string PostTitle { get; set; }

        public DateTime PostDateTime { get; set; }

        public DateTime PostDeleted { get; set; }

        public Guid UserId { get; set; }

        public PostDetails PostDetails { get; set; }
    }

    public class PostDetails
    {

        public Guid PostDetailId { get; set; }

        public Guid PostId { get; set; }

        [Required]
        public string PostText { get; set; }

        public string PostImage { get; set; }
    }
}