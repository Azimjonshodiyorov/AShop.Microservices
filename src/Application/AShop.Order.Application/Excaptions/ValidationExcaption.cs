using FluentValidation.Results;

namespace AShop.Order.Application.Excaptions;

public class ValidationExcaption : ApplicationException
{
   public IDictionary<string , string[]> Error { get; }

   public ValidationExcaption() : base("one or more validation error(s) .")
   {
      Error = new Dictionary<string, string[]>();
   }

   public ValidationExcaption(IEnumerable<ValidationFailure> failures) : this()
   {
      Error = failures.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
         .ToDictionary(x => x.Key, x => x.ToArray());
   }
}