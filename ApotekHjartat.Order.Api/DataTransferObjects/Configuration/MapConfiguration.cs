using ApotekHjartat.Order.Api.DataTransferObjects.ResponseDTOs;
using ApotekHjartat.Order.Api.Repositories.ApotekHjartat;
using AutoMapper;

namespace ApotekHjartat.Order.Api.DataTransferObjects.Configuration
{
    public class MapConfiguration:Profile
    {
        public MapConfiguration()
        {
            CreateMap<Product, ProductResponseDTO>();
            CreateMap<OrderDetail, OrderDetailResponseDTO>();
            CreateMap<Repositories.ApotekHjartat.Order, OrderResponseDTO>();
        }
    }
}
