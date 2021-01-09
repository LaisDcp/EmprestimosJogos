using EmprestimosJogos.Domain.Core.Models;
using EmprestimosJogos.Domain.Validations;
using System;
using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Entities
{
    public class Usuario : Entity
    {
        public Usuario()
        {
            Amigos = new List<Amigo>();
            Tokens = new List<Token>();
            Jogos = new List<Jogo>();
        }

        public string Nome { get; set; }

        public DateTime? ExpirationDate { get; private set; }

        public Guid PerfilId { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual ICollection<Amigo> Amigos { get; set; }

        public virtual ICollection<Token> Tokens { get; set; }

        public virtual ICollection<Jogo> Jogos { get; set; }

        #region Identity

        //
        // Summary:
        //     Gets or sets the date and time, in UTC, when any user lockout ends.
        //
        // Remarks:
        //     A value in the past means the user is not locked out.
        public virtual DateTimeOffset? LockoutEnd { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if two factor authentication is enabled for this
        //     user.
        public virtual bool TwoFactorEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their telephone address.
        public virtual bool PhoneNumberConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets a telephone number for the user.
        public virtual string PhoneNumber { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a user is persisted to the store
        public virtual string ConcurrencyStamp { get; set; }
        //
        // Summary:
        //     A random value that must change whenever a users credentials change (password
        //     changed, login removed)
        public virtual string SecurityStamp { get; set; }
        //
        // Summary:
        //     Gets or sets a salted and hashed representation of the password for this user.
        public virtual string PasswordHash { get; set; }
        //
        // Summary:
        //     Gets or sets a flag indicating if a user has confirmed their email address.
        public virtual bool EmailConfirmed { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized email address for this user.
        public virtual string NormalizedEmail { get; set; }
        //
        // Summary:
        //     Gets or sets the email address for this user.
        public string Email { get; set; }
        //
        // Summary:
        //     Gets or sets the normalized user name for this user.
        public virtual string NormalizedUserName { get; set; }
        //
        // Summary:
        //     Gets or sets the user name for this user.
        public virtual string UserName { get; set; }

        //
        // Summary:
        //     Gets or sets a flag indicating if the user could be locked out.
        public virtual bool LockoutEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the number of failed login attempts for the current user.
        public virtual int AccessFailedCount { get; set; }

        #endregion      

        public void AddNewExpirationDate(int years)
        {
            ExpirationDate = DateTime.Now.AddYears(years);
        }

        /// <summary>
        /// Verifica se a ExpirationDate é menor que a data atual.
        /// </summary>
        /// <returns></returns>
        public bool IsExpiredPassword()
            => ExpirationDate < DateTime.Now;

        public bool IsEmailNotConfirmed()
            => !EmailConfirmed;

        public bool IsLockoutEnable()
            => !LockoutEnabled;

        public void SetUnexpirablePassword()
        {
            ExpirationDate = null;
        }

        public void SetPerfilId(Guid perfilId)
        {
            PerfilId = perfilId;
        }

        /// <summary>
        /// Define que a ExpirationDate será agora, para fins de troca de senha.
        /// </summary>
        public void SetExpiredPassword()
        {
            ExpirationDate = DateTime.Now;
        }

        public Token AddNewToken(string tokenValue, Guid tokenTypeId)
        {
            Tokens ??= new List<Token>();
            var _token = new Token
            {
                TokenTypeId = tokenTypeId,
                Value = tokenValue
            };
            Tokens.Add(_token);
            return _token;
        }

        public void Undelete()
        {
            IsDeleted = false;
        }

        public bool IsValid()
        {
            ValidationResult = new UsuarioValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void SetNome(string nome)
        {
            Nome = nome;
        }
    }
}
