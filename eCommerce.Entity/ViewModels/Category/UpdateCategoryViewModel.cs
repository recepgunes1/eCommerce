namespace eCommerce.Entity.ViewModels.Category
{
    public class UpdateCategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? NewParentCategoryId { get; set; }
        public SimpleCategoryViewModel? ParentCategory { get; set; }
        public IEnumerable<SimpleCategoryViewModel?> Categories { get; set; }
    }
}
