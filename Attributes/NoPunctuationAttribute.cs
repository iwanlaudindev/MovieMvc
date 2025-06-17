using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MvcMovie.Attributes;

public partial class NoPunctuationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string strValue && MyRegex().IsMatch(strValue))
            {
                return new ValidationResult("Judul tidak boleh mengandung tanda baca (punctuation).");
            }

            return ValidationResult.Success;
        }

    [GeneratedRegex(@"[^\w\s]")]
    private static partial Regex MyRegex();
}
