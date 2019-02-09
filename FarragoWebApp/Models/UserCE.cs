using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FarragoWebApp.Models
{
    public class UserCE
    {
        [Display(Name = "Código de Usuario")]
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
        public string Names { get; set; }
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
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BornDate { get; set; }
        [Required]
        [RegularExpression(@"\S+@\S+\.\S{2,3}")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{9}$")]
        [Display(Name = "Número Celular")]
        public string CellphoneNumber { get; set; }
        [Required]
        [Display(Name = "Número de Reservas")]
        public short ReservationsNumber { get; set; }
        [Required]
        [Display(Name = "Reservas Asistidas")]
        public short AssistedNumber { get; set; }
        [Required]
        [Display(Name = "Faltas a Reservas")]
        public short FaultsNumber { get; set; }
    }
    [MetadataType(typeof(UserCE))]
    public partial class User
    {
        [Display(Name = "Cliente")]
        public string FullName { get { return Names + " " + FirstName + " " + LastName; } }
        [Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
        public string LoginErrorMessage { get; set; }
    }
}