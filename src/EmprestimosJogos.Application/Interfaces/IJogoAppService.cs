using EmprestimosJogos.Application.ViewModels;
using System;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IJogoAppService
    {
        ModelCountViewModel<JogoViewModel> GetByFilter(FilterContainsViewModel filter);

        JogoViewModel GetById(Guid id);

        bool Create(JogoViewModel jogo, Guid usuarioId);

        bool Delete(Guid id);

        bool Edit(JogoViewModel jogo, Guid id);
    }
}
