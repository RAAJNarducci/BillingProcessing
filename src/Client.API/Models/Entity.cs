using AutoMapper.Configuration.Annotations;
using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Client.API.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
