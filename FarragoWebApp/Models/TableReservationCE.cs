using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class TableReservationCE
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Fecha de Reservación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateTime { get; set; }
        [Required]
        [Display(Name = "Cliente")]
        public int UserId { get; set; }
        [Required]
        [Display(Name = "Número de Personas")]
        public int PersonsNumber { get; set; }
        [Required]
        [Display(Name = "Confirmación")]
        public string Confirmed { get; set; }
        [Display(Name = "Contacto Interno")]
        public Nullable<int> AdminId { get; set; }
        [Display(Name = "Observaciones")]
        public string UserObservations { get; set; }
        [Display(Name = "Comentarios del Administrador")]
        public string AdminObservations { get; set; }
        [Display(Name = "Estado")]
        public string Status { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual User User { get; set; }
    }
    [MetadataType(typeof(TableReservationCE))]
    public partial class TableReservation
    {
        [Required]
        [Display(Name = "Hora")]
        public int Hour { get; set; }
        [Required]
        [Display(Name = "Minuto")]
        public int Minute { get; set; }
        [Display(Name = "Hora")]
        public string FullHour { get { return DateTime.Hour + ":" + DateTime.Minute; } }
    }
}