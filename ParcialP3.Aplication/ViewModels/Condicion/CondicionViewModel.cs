using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.Condicion
{
    public class CondicionViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? DESCRIPCION { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
