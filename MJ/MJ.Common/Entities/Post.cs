//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MJ.Common.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Post
    {
        public Post()
        {
            this.PostDetails = new HashSet<PostDetail>();
        }
    
        public System.Guid postId { get; set; }
        public string postTitle { get; set; }
        public System.DateTime postDateTime { get; set; }
        public bool postDeleted { get; set; }
        public System.Guid userId { get; set; }
    
        public virtual ICollection<PostDetail> PostDetails { get; set; }
        public virtual SystemUser SystemUser { get; set; }
    }
}