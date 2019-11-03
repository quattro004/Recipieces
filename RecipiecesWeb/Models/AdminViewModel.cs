namespace RecipiecesWeb.Models
{
    public class AdminViewModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public bool IsAdmin { get; set; }
        
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockedOut { get; set; }
    }
}