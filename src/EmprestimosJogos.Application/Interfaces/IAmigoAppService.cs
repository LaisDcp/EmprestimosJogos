using EmprestimosJogos.Application.ViewModels;
using System;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IAmigoAppService
    {
        ModelCountViewModel<AmigoViewModel> GetByFilter(FilterUsuarioViewModel filter);

        AmigoViewModel GetById(Guid id);

        bool Create(AmigoViewModel usuario);

        bool Delete(Guid id);

        bool Edit(AmigoViewModel usuario, Guid id);
    }
}
