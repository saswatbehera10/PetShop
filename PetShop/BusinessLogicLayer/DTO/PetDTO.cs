using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class PetDTO
    {
        public int PetID { get; set; }

        [Required(ErrorMessage ="The Name field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Species field is required.")]
        public string Species { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage ="The price field must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The UserId field is required.")]
        public int UserID { get; set; }
    }
}
