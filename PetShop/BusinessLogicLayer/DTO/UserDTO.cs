using PetShop.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class UserDTO
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
    }
}
