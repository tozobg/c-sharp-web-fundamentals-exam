using System.ComponentModel.DataAnnotations;

namespace TransactionSystem.Core.Utilities
{
    public static class DtoValidator
    {
        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var result in validationResults)
            {
                if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                {
                    Console.WriteLine(result.ErrorMessage);
                }
            }

            return isValid;
        }
    }
}