using System.ComponentModel.DataAnnotations;

namespace ParcialP3.Application.ViewModels.Users
{
    public class RegisterUsersViewModel
    {
        public string? Nombre { get; set; }
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "You must enter the password of user")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        [Required(ErrorMessage = "You must enter the comfirm password")]
        [DataType(DataType.Password)]
        public string? ConfirnPassword { get; set; }
        public string? Email { get; set; }
        public int? Edad { get; set; }
        public int? idEstatus { get; set; }
    }
}
