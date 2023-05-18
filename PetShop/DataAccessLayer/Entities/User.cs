﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PetShop.DataAccessLayer.Entities
{
    public enum UserType
    {
        Admin,
        Customer
    }
    public class User
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

        public virtual ICollection<Pet> Pets { get; set; }

        [ForeignKey("Role")]
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}