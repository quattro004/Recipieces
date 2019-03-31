using Microsoft.AspNetCore.Identity;

namespace RecipiecesWeb.Models
{
    /// <summary>
    /// Extends <see cref="IdentityUser" /> to add additional properties.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser(string email) : base(email) { }
        public ApplicationUser() : base() { }

        [PersonalData]
        public string FirstName { get; set; }
        
        [PersonalData]
        public string LastName { get; set; }
        
    }
}