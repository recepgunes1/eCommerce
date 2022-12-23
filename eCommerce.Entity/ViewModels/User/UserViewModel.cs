namespace eCommerce.Entity.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateBirth { get; set; }
        public string Address { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
        public DateTimeOffset LockoutEnd { get; set; }
    }
}
