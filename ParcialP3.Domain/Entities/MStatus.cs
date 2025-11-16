using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("mSTATUS", Schema = "dbo")]
    public sealed class mSTATUS
    {
        [Key]
        public int Id {  get; set; }
        public string? status { get; set; }

        public ICollection<Users>? Users { get; set; } // Navigation property
    }
}
