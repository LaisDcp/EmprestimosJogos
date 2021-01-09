using EmprestimosJogos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace EmprestimosJogos.Infra.Data.Extensions
{
    public static class SeedDataHelper
    {
        /// Utilize esse método para inserir informações no BD automaticamente através do comando 'Update-Database'
        /// </summary>
        /// <param name="builder"></param> 
        /// <returns></returns>
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            /*
            Nunca alterar os Guids para que não ocorra erros de REFERENCE constraint FKs.
            No momento do Update-Database, pois são os dados que já estão no BD.
            */

            #region TokenType

            builder.Entity<TokenType>()
               .HasData(
                 new TokenType
                 {
                     Id = new Guid("652F2144-7B66-4AC4-967F-E0BAE568EBB1"),
                     Key = TokenType.ResetSenha,
                     Value = "Token para reset de senha (recuperação de senha).",
                     CreatedDate = DateTime.Now
                 });

            #endregion

            #region Perfil

            builder.Entity<Perfil>()
               .HasData(
                 new Perfil
                 {
                     Id = new Guid("8907D860-CEB1-4345-B798-8757200E90C9"),
                     Key = Perfil.ADM,
                     Nome = "Administrador",
                     NormalizedRoleName = "ADMINISTRADOR",
                     Descricao = "Perfil para usuários administradores do sistema.",
                     CreatedDate = DateTime.Now
                 });

            #endregion

            return builder;
        }
    }
}
