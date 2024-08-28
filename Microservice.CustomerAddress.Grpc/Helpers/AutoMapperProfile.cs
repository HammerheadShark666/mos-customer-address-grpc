using Microservice.CustomerAddress.Api.Protos;

namespace Microservice.CustomerAddress.Grpc.Helpers;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        base.CreateMap<Domain.CustomerAddress, CustomerAddressResponse>()
             .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name));
    }
}