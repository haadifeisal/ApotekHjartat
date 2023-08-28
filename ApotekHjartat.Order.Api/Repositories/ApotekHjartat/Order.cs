using ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs;
using System;
using System.Collections.Generic;

namespace ApotekHjartat.Order.Api.Repositories.ApotekHjartat;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public string CreatedAt { get; set; } = null!;

    public string? UpdatedAt { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
