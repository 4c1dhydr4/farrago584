using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class LocalReservationCE
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Fecha de Reservación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Date { get; set; }
        [Required]
        [Display(Name = "Cliente")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Número de Personas")]
        public int PersonsNumber { get; set; }
        [Required]
        [Display(Name = "Confirmación")]
        public string Confirmation { get; set; }
        [Display(Name = "Contacto Interno")]
        public Nullable<int> AdminId { get; set; }
        [Display(Name = "Observaciones")]
        public string UserObservation { get; set; }
        [Display(Name = "Comentarios del Administrador")]
        public string AdminObservation { get; set; }
        [Display(Name = "Costo Total del Evento")]
        public double FinalPrice { get; set; }
        [Display(Name = "Estado")]
        public string Status { get; set; }
    }
    [MetadataType(typeof(LocalReservationCE))]
    public partial class LocalReservation
    {
        [Display(Name = "Lista de Requerimientos")]
        public List<Requirements> RequirementsList { get; set; }
    }
}
