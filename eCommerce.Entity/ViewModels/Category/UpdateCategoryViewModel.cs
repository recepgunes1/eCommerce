namespace eCommerce.Entity.ViewModels.Category
{
    public class UpdateCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IEnumerable<SimpleCategoryViewModel> Categories { get; set; }
    }
}
