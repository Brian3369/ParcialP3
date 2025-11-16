using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("InmuebleImagenes", Schema = "dbo")]
    public sealed class InmuebleImagenes
    {
        public int Id { get; set; }
        public int InmuebleId { get; set; }
        public byte Imagen { get; set; }

        public ICollection<Inmuebles>? Inmuebles { get; set; } // Navigation property
    }
}
