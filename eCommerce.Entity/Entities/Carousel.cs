using eCommerce.Core.Entities;

namespace eCommerce.Entity.Entities
{
    public class Carousel : EntityBase
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
