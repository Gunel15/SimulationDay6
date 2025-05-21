using Microsoft.AspNetCore.Identity;

namespace SimulationDay6.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
