//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Imogen.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRank
    {
        public int pid { get; set; }
        public int UserId { get; set; }
        public int RankId { get; set; }
        public Nullable<int> RankNotesId { get; set; }
        public System.DateTime AwardedOn { get; set; }
    
        public virtual Rank Rank { get; set; }
        public virtual User User { get; set; }
    }
}
