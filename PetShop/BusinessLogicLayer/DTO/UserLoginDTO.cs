using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class UserLoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
