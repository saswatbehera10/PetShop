﻿using System.ComponentModel.DataAnnotations;

namespace PetShop.BusinessLogicLayer.DTO
{
    public class AddAuthResponseDTO
    {

        public string token { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username must be between 1 and 50 characters.", MinimumLength = 1)]

        public string UserName { get; set; }
        public int role { get; set; }
        public int expirationMinutes { get; set; }
        public int userId { get; set; }
    }
}
