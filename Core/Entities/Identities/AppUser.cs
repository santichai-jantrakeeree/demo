using System.ComponentModel.DataAnnotations;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identities
{
    public class AppUser : IdentityUser<int>, IEntity<int>
    {
        [MaxLength(64)]
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}