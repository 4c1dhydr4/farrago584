using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class RequirementsCE
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Requerimiento para Evento")]
        public string ReqName { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Costo")]
        public double Cost { get; set; }
    }
    public class RequirementList
    {
        public List<Requirements> RequirementsList { get; set; }
    }

    [MetadataType(typeof(RequirementsCE))]
    public partial class Requirements
    {
        public bool isChecked { get; set; }
    }
}