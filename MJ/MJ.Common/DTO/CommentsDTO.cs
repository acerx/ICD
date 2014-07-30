using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Common.DTO
{
    public class CommentsDto
    {

        public Guid CommentId { get; set; }

        public string CommentText { get; set; }

        public Guid CommenterId { get; set; }

        public DateTime CommentDateTime { get; set; }

        public bool CommentDeleted { get; set; }

    }
}
