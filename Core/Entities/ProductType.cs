using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}