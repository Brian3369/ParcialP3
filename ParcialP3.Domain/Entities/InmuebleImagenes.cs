using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("InmuebleImagenes", Schema = "dbo")]
    public sealed class InmuebleImagenes
    {
        public int Id { get; set; }
        public string? ImagenURL { get; set; }
        public int InmueblesId { get; set; }

        public Inmuebles? Inmuebles { get; set; } // Navigation property
    }
}
