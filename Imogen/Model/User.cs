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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.EUReporteds = new HashSet<EUReported>();
            this.UsersSessions = new HashSet<UsersSession>();
        }
    
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }
        public string UserDisplayName { get; set; }
        public Nullable<int> UserSex { get; set; }
        public Nullable<int> UserAge { get; set; }
        public Nullable<int> Jurisdiction { get; set; }
        public Nullable<int> Rank { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> FederalJurisdiction { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
        public bool IsOnline { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EUReported> EUReporteds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersSession> UsersSessions { get; set; }
    }
}
