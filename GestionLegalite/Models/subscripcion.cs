//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionLegalite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class subscripcion
    {
        public int id { get; set; }
        public Nullable<int> idpost { get; set; }
        public Nullable<int> idasesor { get; set; }
    
        public virtual asesore asesore { get; set; }
        public virtual post post { get; set; }
    }
}
