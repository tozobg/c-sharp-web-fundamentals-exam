using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TransactionSystem.Core.Utilities
{
    public static class DtoValidator
    {
        public static bool IsValid(object dto)
        {
            StringBuilder resultMessages = new();

            ValidationContext validationContext = new(dto);
            List<ValidationResult> validationResults = new();

            bool isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            foreach (var result in validationResults)
            {
                if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
                {
                    resultMessages.AppendLine(result.ErrorMessage);
                }
            }

            if (!isValid)
            {
                throw new Exception(resultMessages.ToString());
            }

            return isValid;
        }
    }
}