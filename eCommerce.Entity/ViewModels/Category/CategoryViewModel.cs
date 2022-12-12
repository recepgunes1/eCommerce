namespace eCommerce.Entity.ViewModels.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public SimpleCategoryViewModel? ParentCategory { get; set; }
    }
}
