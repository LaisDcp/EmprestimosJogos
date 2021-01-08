namespace EmprestimosJogos.Domain.Core.DTO
{
    public class ExceptionMessageDTO
    {
        public ExceptionMessageDTO()
        { }
        public ExceptionMessageDTO(string field, string message)
        {
            Field = field;
            Message = message;
        }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public string Field { get; set; }
    }
}
