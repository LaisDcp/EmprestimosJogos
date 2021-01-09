using AutoMapper;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Entities;

namespace EmprestimosJogos.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<NomeBaseViewModel, Jogo>()
                .ForMember(entity => entity.Id, opt => opt.Ignore());

            CreateMap<AmigoViewModel, Amigo>()
                .ForMember(entity => entity.Id, opt => opt.Ignore());

            CreateMap<CreateUsuarioViewModel, Usuario>()
                .ForMember(entity => entity.Id, opt => opt.Ignore())
                .ForMember(entity => entity.UserName, opt => opt.MapFrom(viewModel => viewModel.Email));
        }
    }
}
