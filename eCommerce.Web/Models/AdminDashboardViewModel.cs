namespace eCommerce.Web.Models
{
    public class AdminDashboardViewModel
    {
        public (int ParentDeleted, int ParentNonDeleted, int ChildDeleted, int ChildNonDeleted) Category { get; set; }
        public (int DeletedInStock, int NonDeletedInStock, int NonDeletedOutOfStock) Product { get; set; }
        public (int Deleted, int Visible, int Invisible) Comment { get; set; }
    }
}
