using EmprestimosJogos.Domain.Entities;
using EmprestimosJogos.Domain.Interfaces.Repositories;
using EmprestimosJogos.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmprestimosJogos.Infra.CrossCutting.Identity.Services
{
    public class UsuarioStore : IUserStore<Usuario>,
        IUserPasswordStore<Usuario>,
        IUserSecurityStampStore<Usuario>,
        IUserLockoutStore<Usuario>,
        IUserEmailStore<Usuario>,
        IUserPhoneNumberStore<Usuario>
    {
        private readonly EmprestimosJogosContext _context;
        private readonly IUsuarioRepository _repository;

        public UsuarioStore(EmprestimosJogosContext context,
                            IUsuarioRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public Task<string> GetUserIdAsync(Usuario auth, CancellationToken cancellationToken)
        {
            return Task.FromResult(auth.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario auth, CancellationToken cancellationToken)
        {
            return Task.FromResult(auth.UserName);
        }

        public Task SetUserNameAsync(Usuario auth, string Login, CancellationToken cancellationToken)
        {
            auth.UserName = Login;
            return Task.FromResult(auth);
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario auth, CancellationToken cancellationToken)
        {
            return Task.FromResult(auth.UserName.Trim().ToUpper());
        }

        public Task SetNormalizedUserNameAsync(Usuario auth, string normalizedName, CancellationToken cancellationToken)
        {
            auth.NormalizedUserName = normalizedName.Trim().ToUpper();
            return Task.FromResult(auth);
        }

        public async Task<IdentityResult> CreateAsync(Usuario auth, CancellationToken cancellationToken)
        {
            _repository.Create(auth);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateAsync(Usuario auth, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            _repository.Update(auth);

            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(Usuario auth, CancellationToken cancellationToken)
        {
            _context.Remove(auth);

            int i = await _context.SaveChangesAsync(cancellationToken);

            return await Task.FromResult(i == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Usuario.FirstOrDefaultAsync(wh => wh.Id == Guid.Parse(userId));
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return await _context.Usuario.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }

        public Task SetPasswordHashAsync(Usuario auth, string passwordHash, CancellationToken cancellationToken)
        {
            auth.PasswordHash = passwordHash;

            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(Usuario auth, CancellationToken cancellationToken)
        {
            return Task.FromResult(auth.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Usuario auth, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(auth.PasswordHash));
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

        /// <summary>
        ///     Set the security stamp for the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetSecurityStampAsync(Usuario auth, string stamp, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the security stamp for a user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetSecurityStampAsync(Usuario auth, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.SecurityStamp);
        }

        /// <summary>
        ///     Locks a user out until the specified end date (set to a past date, to unlock a user)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEndDateAsync(Usuario auth, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.LockoutEnd = lockoutEnd;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Used to record when an attempt to access the user has failed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> IncrementAccessFailedCountAsync(Usuario auth, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.AccessFailedCount++;
            return Task.FromResult(auth.AccessFailedCount);
        }

        /// <summary>
        ///     Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered
        ///     not locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<DateTimeOffset?> GetLockoutEndDateAsync(Usuario auth, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.LockoutEnd);
        }

        /// <summary>
        ///     Returns the current number of failed access attempts.  This number usually will be reset whenever the password is
        ///     verified or the account is locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> GetAccessFailedCountAsync(Usuario auth, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.AccessFailedCount);
        }

        /// <summary>
        ///     Returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetLockoutEnabledAsync(Usuario auth, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.LockoutEnabled);
        }

        /// <summary>
        ///     Sets whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEnabledAsync(Usuario auth, bool enabled, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Used to reset the account access count, typically after the account is successfully accessed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task ResetAccessFailedCountAsync(Usuario auth, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.AccessFailedCount = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set the user email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetEmailAsync(Usuario auth, string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.Email = email;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get the user's email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetEmailAsync(Usuario auth, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.Email);
        }

        public virtual Task<string> GetNormalizedEmailAsync(Usuario auth, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.NormalizedEmail);
        }

        public virtual Task SetNormalizedEmailAsync(Usuario auth, string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.NormalizedEmail = normalizedEmail;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Find an user by email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<Usuario> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            return _context.Usuario.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail, cancellationToken);
        }

        /// <summary>
        ///     Returns whether the user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetEmailConfirmedAsync(Usuario auth, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.EmailConfirmed);
        }

        /// <summary>
        ///     Set IsConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetEmailConfirmedAsync(Usuario auth, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Set the user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberAsync(Usuario auth, string phoneNumber, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.PhoneNumber = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        ///     Get a user's phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<string> GetPhoneNumberAsync(Usuario auth, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.PhoneNumber);
        }

        /// <summary>
        ///     Returns whether the user phoneNumber is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(Usuario auth, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            return Task.FromResult(auth.PhoneNumberConfirmed);
        }

        /// <summary>
        ///     Set PhoneNumberConfirmed on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberConfirmedAsync(Usuario auth, bool confirmed, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (auth == null)
                throw new ArgumentNullException(nameof(auth));

            auth.PhoneNumberConfirmed = confirmed;
            return Task.FromResult(0);
        }
    }
}
