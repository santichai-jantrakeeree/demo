using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class ProductTypeDto : IDto<int>
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }
    }
}