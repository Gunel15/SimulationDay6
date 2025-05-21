using System.ComponentModel.DataAnnotations;

namespace SimulationDay6.ViewModels.Account
{
    public class RegisterVM
    {
        [MinLength(5),MaxLength(30)]
        public string FullName {  get; set; }
        [MinLength(5), MaxLength(30)]
        public string Username {  get; set; }
        [ MaxLength(30),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(30), DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(30), DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmedPassword {  get; set; }
    }
}
