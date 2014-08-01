using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Common.DTO
{
    public class PostsDto
    {

        public Guid PostId { get; set; }

        public string PostTitle { get; set; }

        public DateTime PostDateTime { get; set; }

        public bool PostDeleted { get; set; }

        public Guid UserId { get; set; }

        public PostDetailsDto PostDetailsDto { get; set; }

    }
}
