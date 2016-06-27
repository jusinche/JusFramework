using System.ComponentModel.DataAnnotations;

namespace JusUserFront.UI.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar clave?")]
        public bool RememberMe { get; set; }

        public bool ShowMessage { get; set; }
    }
}