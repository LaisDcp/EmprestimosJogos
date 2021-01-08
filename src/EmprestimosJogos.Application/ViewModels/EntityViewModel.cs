namespace EmprestimosJogos.Application.ViewModels
{
    public class EntityViewModel
    {
        public EntityViewModel() { }

        public EntityViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}