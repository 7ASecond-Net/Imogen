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
    
    public partial class Hash
    {
        public int pid { get; set; }
        public int id { get; set; }
        public string HashType { get; set; }
        public string HashValue { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    
        public virtual EUReported EUReported { get; set; }
    }
}
