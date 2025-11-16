using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("Ciudades", Schema = "dbo")]
    public sealed class Ciudades
    {
        [Key]
        public int Id { get; set; }
        public string NOMBRE { get; set; }

        public ICollection<Inmuebles>? Inmuebles { get; set; } // Navigation property
    }
}
