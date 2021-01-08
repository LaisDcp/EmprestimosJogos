namespace EmprestimosJogos.Domain.Core.DTO
{
    public class TokenConfigurationsDTO
    {
        public string Secret { get; set; }

        public double ExpiresIn { get; set; }
    }
}
