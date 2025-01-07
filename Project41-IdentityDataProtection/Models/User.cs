using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Project41_IdentityDataProtection.Models
{
    public class User : IdentityUser
    {
        internal string UserName;

        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public override string Email { get; set; } = string.Empty;
    }

}

