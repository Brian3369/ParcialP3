using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParcialP3.Domain.Entities
{
    [Table("mSTATUS", Schema = "dbo")]
    public sealed class MStatus
    {
        [Key]
        public int idStatus {  get; set; }
        public string status { get; set; }
    }
}
