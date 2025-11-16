using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("TipoPropiedad", Schema = "dbo")]
    public sealed class TipoPropiedad
    {
        [Key]
        public int Id { get; set; }
        public string DESCRIPCION { get; set; }
        public bool? Activo { get; set; }

        public ICollection<Inmuebles>? Inmuebles { get; set; } // Navigation property
    }
}
