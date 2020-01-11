using System.ComponentModel.DataAnnotations;

namespace API.Domain.DTOs
{
    public class UserDTOCreate
    {
        [Required(ErrorMessage = "Nome é um campo obrigatório!")]
        [StringLength(60, ErrorMessage = "O tamanho maximo para nome é de {1} caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "E-mail é um campo obrigatório para Login")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido.")]
        [StringLength(100, ErrorMessage = "E-mail deve ter no maximo {1} caracteres.")]
        public string Email { get; set; }
    }
}