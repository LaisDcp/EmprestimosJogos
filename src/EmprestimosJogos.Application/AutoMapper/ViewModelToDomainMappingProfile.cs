using AutoMapper;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Entities;

namespace EmprestimosJogos.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<JogoViewModel, Jogo>()
                .ForMember(entity => entity.AmigoId, opt => opt.Ignore())
                .ForMember(entity => entity.Amigo, opt => opt.Ignore());

            CreateMap<AmigoViewModel, Amigo>();
        }
    }
}
