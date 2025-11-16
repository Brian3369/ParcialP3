using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.TipoPropiedad
{
    public class TipoPropiedadViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string DESCRIPCION { get; set; }
        [Required]
        public bool? Activo { get; set; }
    }
}
