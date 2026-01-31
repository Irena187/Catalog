using Microsoft.AspNetCore.Identity;

namespace Catalog.Data.Models
{
    public class Userr : IdentityUser
    {
        
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
