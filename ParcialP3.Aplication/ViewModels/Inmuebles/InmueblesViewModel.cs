using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.Inmuebles
{
    public class InmueblesViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NombreInmueble { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int TipoPropiedadId { get; set; }
        public string? TipoPropiedad { get; set; }
        [Required]
        public int CondicionId { get; set; }
        public string? Condicion { get; set; }
        [Required]
        public int CiudadesId { get; set; }
        public string? Ciudades { get; set; }
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
