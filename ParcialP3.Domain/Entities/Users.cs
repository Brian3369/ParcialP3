using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;

namespace ParcialP3.Domain.Entities
{
    [Table("USERS", Schema = "dbo")]
    public sealed class Users
    {
        [Key]
        public int ID { get; set; }
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int? Edad {  get; set; }
        public int? idEstatus { get; set; }

        [ForeignKey(nameof(idEstatus))]
        public mSTATUS? mSTATUS { get; set; } // Navigation property
    }
}
