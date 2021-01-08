using EmprestimosJogos.Domain.Core.DTO;

namespace EmprestimosJogos.Domain.Interfaces.Facades
{
    public interface ITokenFacade
    {
        string GenerateToken(ContextUserDTO user, double? minutesExpiration = null);
    }
}
