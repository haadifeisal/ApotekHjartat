using ApotekHjartat.Order.Api.Enums;
using ApotekHjartat.Order.Api.Helpers;

namespace ApotekHjartat.Order.Api.Repositories.ApotekHjartat
{
    public class SeedDB
    {
        public static void SeedData(ApotekHjartatContext context)
        {
            if (!context.Products.Any())
            {
                System.Console.WriteLine("\n\nAdding data - Seeding ...\n\n");

                // Products
                var product1 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Ipren 500mg",
                    Price = 60
                };
                context.Products.Add(product1);

                var product2 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Colgate Tandkram",
                    Price = 15
                };
                context.Products.Add(product2);

                var product3 = new Product
                {
                    ProductId = Guid.NewGuid(),
                    Name = "Fungoral Schampoo",
                    Price = 99
                };
                context.Products.Add(product3);

                // Order
                var order = new Order
                {
                    OrderId = Guid.NewGuid(),
                    UserId = Guid.Parse("44e4cb84-2b8b-4ee7-8f0c-a77ca815709a"),
                    Status = (int)Status.Pending,
                    CreatedAt = Helper.ConvertDateTimeToString(DateTime.Now)
                };
                context.Orders.Add(order);

                // OrderItem
                var orderItem1 = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = product1.ProductId,
                    Quantity = 3
                };
                context.OrderItems.Add(orderItem1);

                var orderItem2 = new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    ProductId = product3.ProductId,
                    Quantity = 1
                };
                context.OrderItems.Add(orderItem2);
                context.SaveChanges();

                System.Console.WriteLine("\n\n Seeding Completed!! ...\n\n");
            }
            else
            {
                System.Console.WriteLine("Already have data - not seeding");
            }
        }
    }
}
