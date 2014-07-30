using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Common.DTO
{
    public class PostDetailsDto
    {

        public Guid PostDetailId { get; set; }

        public Guid PostId { get; set; }

        public string PostText { get; set; }

        public string PostImage { get; set; }

    }
}
