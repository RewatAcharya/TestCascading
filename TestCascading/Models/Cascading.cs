using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCascading.Models
{
    public class Cascading
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(State))]
        public Guid StateId { get; set; }
        public virtual State State { get; set; }

        [Required]
        [ForeignKey("District")]
        public Guid DistrictId { get; set; }
        public virtual District District { get; set; }

        [Required]
        [ForeignKey("Ward")]
        public Guid WardId { get; set; }
        public virtual Ward Ward { get; set; }
    }
}
