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
    
    public partial class PostDetail
    {
        public System.Guid postDetailId { get; set; }
        public System.Guid postId { get; set; }
        public string postText { get; set; }
        public string postImage { get; set; }
    
        public virtual Post Post { get; set; }
    }
}
