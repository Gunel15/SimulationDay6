namespace SimulationDay6.Models
{
    public class Position:BaseEntity
    {
        public string Name {  get; set; }
        public IEnumerable<Person> Persons { get; set; }
    }
}
