using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetShop.DataAccessLayer.Entities
{
    public class Pet
    {
        [Key]
        public int PetID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public string ImgUrl { get; set; }

        public virtual Order Order { get; set; }
    }
}
