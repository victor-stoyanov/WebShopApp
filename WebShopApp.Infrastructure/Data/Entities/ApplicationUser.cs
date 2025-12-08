using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebShopApp.Infrastructure_.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Address { get; set; } = null!;
    }
}
