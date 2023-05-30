using System.ComponentModel.DataAnnotations;

namespace PetShop.DataAccessLayer.Entities
{
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}