using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductBrand : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}