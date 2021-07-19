using Core.Entities.Identities;
using Core.Interfaces;

namespace Core.Dtos
{
    public class AppUserDto : IDto<int>
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public AddressDto Address { get; set; }
    }
}