namespace EmprestimosJogos.Domain.Core.DTO
{
    public class AuthReturnDTO
    {
        public string Token { get; set; }

        public string TokenType { get; set; }

        public bool IsAuthenticated { get; set; }

        public ContextUserDTO User { get; set; }
    }
}
