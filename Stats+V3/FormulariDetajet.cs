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
    
    public partial class FormulariDetajet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormulariDetajet()
        {
            this.SuksesiLendors = new HashSet<SuksesiLendor>();
        }
    
        public int Id { get; set; }
        public int FormulariId { get; set; }
        public Nullable<int> OretEPlanifikuara { get; set; }
        public Nullable<int> OretEMbajtura { get; set; }
    
        public virtual Formulari Formulari { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuksesiLendor> SuksesiLendors { get; set; }
    }
}