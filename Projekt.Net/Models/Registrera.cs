using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projekt.Net.Models
{
    public class Registrera
    {
        [Key]
        public int UserID { get; set; }
        [Required(ErrorMessage = "Du måste ange ett användarnamn.")]
        [MaxLength(20)]
        public string Användarnamn { get; set; }
        [Required(ErrorMessage = "Du måste ange en email.")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [MaxLength(300)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Du måste ange ett lösenord.")]
        [DataType(DataType.Password)]
        [MaxLength(15)]
        public string Lösenord { get; set; }
        [Compare("Lösenord", ErrorMessage = "Lösenorden matchar inte.")]
        [DataType(DataType.Password)]
        [MaxLength(15)]
        public string RepeteraLösenord { get; set; }
    }
}