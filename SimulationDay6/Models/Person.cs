using System.ComponentModel.DataAnnotations;

namespace SimulationDay6.Models
{
    public class Person:BaseEntity
    {
        [MinLength(2),MaxLength(20)]
        public string Name {  get; set; }
        public Position? Position { get; set; }
        public int PositionId {  get; set; }
        [MinLength(5), MaxLength(50)]
        public string Description {  get; set; }
        public string ImageUrl {  get; set; }
    }
}
