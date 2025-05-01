using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace ToDoApp.Application.Tasks.Behavior
{
    //// Detta är ett MediatR pipeline-behavior som kör validering före varje handler
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        //// Konstruktor – injicerar alla registrerade validatorer för denna typ av request
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        // Detta körs innan varje MediatR-handler anropas
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()) //om det finns någon validator registrerad
            {
                // Skapa ett valideringssammanhang baserat på request-objektet
                var context = new ValidationContext<TRequest>(request);
                //kör alla validatorer parallelt
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                //samlar ihop alla fel
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0) //om det finns, kasta in undantag så att handlern inte körs
                    throw new ValidationException(failures);
            }

            return await next(); // om allt är ok så gå vidare till nästa steg i pipeline (själva handler)
        }
    }
}
