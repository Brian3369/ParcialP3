using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("Inmuebles", Schema = "dbo")]
    public sealed class Inmuebles
    {
        [Key]
        public int Id { get; set; }
        public string NombreInmueble { get; set; }
        public string Direccion { get; set; }
        public int TipoPropiedadId { get; set; }
        public TipoPropiedad? TipoPropiedad { get; set; } // Navigation property
        public int CondicionId { get; set; }
        public Condicion? Condicion { get; set; } // Navigation property
        public int CiudadId { get; set; }
        public Ciudades? Ciudades { get; set; } // Navigation property
        public int? Inmuebleimagenid { get; set; }
        public ICollection<InmuebleImagenes>? InmuebleImagenes { get; set; } // Navigation property
        public double Precio { get; set; }
        public int? Habitaciones { get; set; }
        public int Baños { get; set; }
        public string? Descripcion { get; set; }
        public string? TipoNegocio { get; set; }
        public bool Activo { get; set; }
    }
}
