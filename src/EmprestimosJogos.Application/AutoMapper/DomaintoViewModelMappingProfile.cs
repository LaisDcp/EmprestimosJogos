using AutoMapper;
using EmprestimosJogos.Application.ViewModels;
using EmprestimosJogos.Domain.Entities;

namespace EmprestimosJogos.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Jogo, JogoViewModel>();

            CreateMap<Amigo, AmigoViewModel>();

            CreateMap<Amigo, NomeBaseViewModel>();

            CreateMap<Usuario, UsuarioViewModel>();
        }
    }
}