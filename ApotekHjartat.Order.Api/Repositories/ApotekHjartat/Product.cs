using System;
using System.Collections.Generic;

namespace ApotekHjartat.Order.Api.Repositories.ApotekHjartat;

public partial class Product
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
