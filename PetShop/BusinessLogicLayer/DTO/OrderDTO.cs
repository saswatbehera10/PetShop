using PetShop.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class OrderDTO
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [ForeignKey("Pet")]
        public int PetID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
    }
}
