using System.ComponentModel.DataAnnotations;

namespace SimulationDay6.ViewModels.Persons
{
    public class PersonUpdateVM
    {
        public int Id { get; set; }
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        public int PositionId { get; set; }
        [MinLength(5), MaxLength(50)]
        public string Description { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
