using System.ComponentModel.DataAnnotations;

namespace API.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é um campo obrigatorio para login.")]
        [EmailAddress(ErrorMessage = "Email em formato invalido")]
        [StringLength(100, ErrorMessage = "Email dever ter no maximo {1} caracteres.")]
        public string Email { get; set; }

    }
}