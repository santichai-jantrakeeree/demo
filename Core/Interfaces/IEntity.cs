using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Interfaces
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        [Key]
        TKey Id { get; set; }
    }
}