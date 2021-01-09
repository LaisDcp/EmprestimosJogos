using System;

namespace EmprestimosJogos.Application.ViewModels
{
    public class JogoViewModel : EntityViewModel
    {
        public JogoViewModel()
        {
            
        }

        public DateTime? DataUltimoEmprestimo { get; set; }

        public bool IsEmprestado { get; set; }

        public Guid? AmigoId { get; set; }

        public NomeBaseViewModel Amigo { get; set; }
    }
}