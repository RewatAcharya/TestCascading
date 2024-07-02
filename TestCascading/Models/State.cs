using System.ComponentModel.DataAnnotations;

namespace TestCascading.Models
{
    public class State
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
