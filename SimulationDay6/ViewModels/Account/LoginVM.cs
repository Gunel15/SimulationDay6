using System.ComponentModel.DataAnnotations;

namespace SimulationDay6.ViewModels.Account
{
    public class LoginVM
    {
        [MaxLength(30), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(30), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
