using eCommerce.Core.Entities;

namespace Database.Models
{
    public class Image : EntityBase
    {
        public byte[]? Picture { get; set; }
    }
}
