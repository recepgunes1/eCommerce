namespace eCommerce.Entity.ViewModels.Brand
{
    public class BrandViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Entities.Image Image { get; set; }
    }
}
