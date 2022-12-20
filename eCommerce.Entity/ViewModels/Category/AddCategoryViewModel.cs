namespace eCommerce.Entity.ViewModels.Category
{
    public class AddCategoryViewModel
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IEnumerable<CategoryViewModel> ParentCategories { get; set; }
    }
}
