//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcInlämningsuppgift2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bestallning
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bestallning()
        {
            this.BestallningMatratts = new HashSet<BestallningMatratt>();
        }
    
        public int BestallningID { get; set; }
        public System.DateTime BestallningDatum { get; set; }
        public int Totalbelopp { get; set; }
        public bool Levererad { get; set; }
        public int KundID { get; set; }
    
        public virtual Kund Kund { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BestallningMatratt> BestallningMatratts { get; set; }
    }
}
