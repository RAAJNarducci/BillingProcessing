using AutoMapper;
using Client.API.Models;

namespace Client.API.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClientViewModel, Models.Client>()
               .ConstructUsing(c => new Models.Client(c.Nome, c.Estado, long.Parse(c.Cpf, 0)));

            CreateMap<Models.Client, ClientViewModel>();

        }
    }
}
