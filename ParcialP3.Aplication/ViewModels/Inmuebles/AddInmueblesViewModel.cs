using ParcialP3.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.Inmuebles
{
    public class AddInmueblesViewModel
    {
        [Required]
        public string NombreInmueble { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int TipoPropiedadId { get; set; }
        public List<Domain.Entities.TipoPropiedad>? TipoPropiedad { get; set; }
        [Required]
        public int CondicionId { get; set; }
        public List<Domain.Entities.Condicion>? Condicion { get; set; }
        [Required]
        public int CiudadesId { get; set; }
        public List<Domain.Entities.Ciudades>? Ciudades { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public int? Habitacion { get; set; }
        [Required]
        public int Baños { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public string? TipoNegocio { get; set; }
        [Required]
        public bool Activo { get; set; }
        public List<string>? Imagenes { get; set; }
    }
}
