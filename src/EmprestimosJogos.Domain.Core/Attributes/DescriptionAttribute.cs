using System;

namespace EmprestimosJogos.Domain.Core.Attributes
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(
            string description,
            string title = null
            )
        {
            Description = description;
            Title = title;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}