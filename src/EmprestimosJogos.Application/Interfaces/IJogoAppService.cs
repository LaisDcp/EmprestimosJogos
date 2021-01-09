using EmprestimosJogos.Application.ViewModels;
using System;

namespace EmprestimosJogos.Application.Interfaces
{
    public interface IJogoAppService
    {
        ModelCountViewModel<JogoViewModel> GetByFilter(FilterPaginacaoViewModel filter, Guid usuarioId);

        JogoViewModel GetById(Guid id);

        bool Create(NomeBaseViewModel jogo, Guid usuarioId);

        bool Delete(Guid id);

        bool Edit(NomeBaseViewModel jogo, Guid id);

        bool Emprestar(Guid id, Guid amigoId);

        bool Devolver(Guid id);
    }
}
