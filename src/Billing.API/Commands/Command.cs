using FluentValidation.Results;
using System;
using System.Text.Json.Serialization;

namespace BillingAPI.Commands
{
    public abstract class Command
    {
        [JsonIgnore]
        public ValidationResult ValidationResult { get; set; }
        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
