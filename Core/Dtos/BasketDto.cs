using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Interfaces;

namespace Core.Dtos
{
    public class BasketDto : IDto<string>
    {
        [Required]        
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}