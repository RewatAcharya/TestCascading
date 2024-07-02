using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCascading.Models
{
    public class District
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(State))]
        public Guid StateFK { get; set; }

        public virtual State State { get; set; }
    }
}
