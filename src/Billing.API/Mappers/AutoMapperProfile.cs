using AutoMapper;
using BillingAPI.Commands;
using BillingAPI.Models;
using BillingAPI.ViewModels;

namespace BillingAPI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewBillingCommand, Billing>()
                    .ForMember(dst => dst.Cpf,
                    map => map.MapFrom(src => long.Parse(src.Cpf)));
        }
    }
}
