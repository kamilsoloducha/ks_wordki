using System;
using System.Linq;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
    {
        private readonly string _name;
        private readonly IValidator<TOptions> _validator;

        public FluentValidationOptions(string name, IValidator<TOptions> validator)
        {
            _name = name;
            _validator = validator;
        }

        public ValidateOptionsResult Validate(string name, TOptions options)
        {
            if (_name != null && _name != name)
                return ValidateOptionsResult.Skip;

            ArgumentNullException.ThrowIfNull(options);

            var result = _validator.Validate(options);

            return result.IsValid
                ? ValidateOptionsResult.Success
                : ValidateOptionsResult.Fail(result.Errors.Select(x => $"{typeof(TOptions).Name}: {x.ErrorMessage}"));
        }
    }
}