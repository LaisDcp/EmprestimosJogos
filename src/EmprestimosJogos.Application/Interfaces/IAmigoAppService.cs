using EmprestimosJogos.Application.ViewModels;
using System;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IAmigoAppService
    {
        ModelCountViewModel<AmigoViewModel> GetByFilter(FilterContainsViewModel filter);

        AmigoViewModel GetById(Guid id);

        bool Create(AmigoViewModel amigo);

        bool Delete(Guid id);

        bool Edit(AmigoViewModel amigo, Guid id, Guid usuarioId);
    }
}
