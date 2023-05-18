using PetShop.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public enum UserType
    {
        Admin,
        Customer
    }
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

        [Required]
        public UserType UserType { get; set; }

        //public virtual ICollection<Pet> Pets { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
    }
}
