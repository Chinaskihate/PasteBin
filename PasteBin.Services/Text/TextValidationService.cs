using PasteBin.Contracts.Text.Validation;
using System.ComponentModel.DataAnnotations;

namespace PasteBin.Services.Text;
public class TextValidationService(int maxLength) : ITextValidationService
{
    private readonly int _maxLength = maxLength;

    public void ValidateText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ValidationException($"Text must be not empty");
        }

        if (text.Length > _maxLength)
        {
            throw new ValidationException($"Number of characters should be less than {_maxLength}");
        }
    }
}
