namespace eCommerce.Entity.ViewModels.User
{
    public class SimpleUserViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public Guid RoleId { get; set; }
    }
}
