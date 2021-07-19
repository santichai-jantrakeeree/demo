using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Entities
{
    public class Basket : IEntity<string>
    {
        public Basket()
        {
        }

        public Basket(string id)
        {
            this.Id = id;

        }

        public Basket(string id, List<BasketItem> items) : this(id)
        {
            Items = items;
        }

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}