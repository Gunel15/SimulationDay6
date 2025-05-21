using SimulationDay6.ViewModels.Persons;
using SimulationDay6.ViewModels.Positions;

namespace SimulationDay6.ViewModels
{
    public class HomeVM
    {
       public List<PersonGetVM> Persons { get; set; }
       public List<PositionGetVM> Positions { get; set; }
    }
}
