using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class AdminCE
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Usuario")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Display(Name = "Nombres")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Display(Name = "Apellido Paterno")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z'\s]*$")]
        [Display(Name = "Apellido Materno")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{8}$")]
        [Display(Name = "DNI")]
        public string DNI { get; set; }
        [Required]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Número Celular")]
        public string CellphoneNumber { get; set; }
        [Required]
        [Display(Name = "Super Usuario")]
        public string isSuperuser { get; set; }
        [Required]
        [Display(Name = "Activo")]
        public string isActive { get; set; }
    }
    [MetadataType(typeof(AdminCE))]
    public partial class Admin
    {
        [Display(Name = "Administrador")]
        public string FullName { get { return Name + " " + FirstName + " " + LastName; } }
        public string LoginErrorMessage { get; set; }
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
        public List<TableReservation> tableReservationsPend { get; set; }
        public List<LocalReservation> localReservationsPend { get; set; }
    }
}