using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.Inmuebles
{
    public class InmuebleViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NombreInmueble { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public int TipoPropiedadId { get; set; }
        [Required]
        public int CondicionId { get; set; }
        [Required]
        public int CiudadId { get; set; }
        [Required]
        public int? Inmuebleimagenid { get; set; }
        [Required]
        public double Precio { get; set; }
        [Required]
        public int? Habitaciones { get; set; }
        [Required]
        public int Baños { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public string? TipoNegocio { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
