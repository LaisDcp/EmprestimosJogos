using System.Collections.Generic;

namespace EmprestimosJogos.Domain.Core.DTO
{
    public class ExceptionDetailsDTO
    {
        public int HttpStatusCode { get; set; }

        public string Message { get; set; }

        public IEnumerable<ExceptionMessageDTO> Errors { get; set; }
    }
}
