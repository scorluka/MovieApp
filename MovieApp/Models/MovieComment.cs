//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovieComment
    {
        public int CommentId { get; set; }
        public string Comments { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> MovieId { get; set; }
    }
}
