using Microsoft.EntityFrameworkCore;

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
            return builder;
        }
    }
}
