using System.ComponentModel.DataAnnotations;

namespace SimulationDay6.ViewModels.Persons
{
    public class PersonGetVM
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        public string PositionName { get; set; }
        [MinLength(5), MaxLength(50)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
