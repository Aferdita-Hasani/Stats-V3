//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stats_V3
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserShkolla
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ShkollaId { get; set; }
    
        public virtual Shkolla Shkolla { get; set; }
    }
}