using EmprestimosJogos.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.Identity.Services
{
    public class PerfilStore : IRoleStore<Perfil>
    {

        private readonly EmprestimosJogosContext _context;
        private readonly IPerfilRepository _repository;

        public PerfilStore(EmprestimosJogosContext context,
                           IPerfilRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IdentityResult> CreateAsync(Perfil role, CancellationToken cancellationToken)
        {
            _context.Add(role);

            await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(Perfil role, CancellationToken cancellationToken)
        {
            _context.Remove(role);

            int i = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context?.Dispose();
        }

        public async Task<Perfil> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _context.Perfis.FirstOrDefaultAsync(wh => wh.Id == Guid.Parse(roleId));
        }

        public async Task<Perfil> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Perfis.FirstOrDefaultAsync(u => u.Descricao == normalizedRoleName, cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Descricao.Trim().ToUpper());
        }

        public Task<string> GetRoleIdAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(Perfil role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Descricao);
        }

        public Task SetNormalizedRoleNameAsync(Perfil role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedRoleName = normalizedName.Trim().ToUpper();
            return Task.FromResult(role);
        }

        public Task SetRoleNameAsync(Perfil role, string roleName, CancellationToken cancellationToken)
        {
            role.Descricao = roleName;
            return Task.FromResult(role);
        }

        public async Task<IdentityResult> UpdateAsync(Perfil role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            bool _result = _repository.UpdateAsync(role).Result;

            return await Task.FromResult(IdentityResult.Success);
        }
    }
}
