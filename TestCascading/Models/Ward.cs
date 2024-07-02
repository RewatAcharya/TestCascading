using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCascading.Models
{
    public class Ward
    {
        [Key]
        public Guid Id { get; set; }

        public int WardNo { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(District))]
        public Guid DistrictFK { get; set; }

        public virtual District District { get; set; }
    }
}
