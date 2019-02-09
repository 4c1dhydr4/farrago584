using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class LocalRequirementsCE
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Código de Reservación")]
        public int LocalReservation { get; set; }
        [Required]
        [Display(Name = "Requerimiento")]
        public int Requirement { get; set; }
    }

    [MetadataType(typeof(LocalRequirementsCE))]
    public partial class LocalRequirements
    {
    }
}