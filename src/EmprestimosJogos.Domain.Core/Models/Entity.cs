using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosJogos.Domain.Core.Models
{
    public abstract class Entity
    {
        protected Entity()
        {
            IsDeleted = false;
            CreatedDate = DateTime.Now;
        }
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool IsDeleted { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public void MarkDeleted()
        {
            IsDeleted = true;
            ModifiedDate = DateTime.Now;
        }
    }
}
