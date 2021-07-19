using Core.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class DeliveryMethodDto : IDto<int>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
        [DisplayName("Delivery Time")]

        [Required]
        [MaxLength(256)]
        public string DeliveryTime { get; set; }

        [Required]
        [MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}