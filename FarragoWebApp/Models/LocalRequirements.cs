//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FarragoWebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocalRequirements
    {
        public int id { get; set; }
        public int LocalReservation { get; set; }
        public int Requirement { get; set; }
    
        public virtual LocalReservation LocalReservation1 { get; set; }
        public virtual Requirements Requirements { get; set; }
    }
}