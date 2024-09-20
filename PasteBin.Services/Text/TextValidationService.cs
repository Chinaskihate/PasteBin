using PasteBin.Contracts.Text.Validation;
using System.ComponentModel.DataAnnotations;
using PasteBin.Resources.Errors;

namespace PasteBin.Services.Text;
public class TextValidationService(int maxLength) : ITextValidationService
{
    private readonly int _maxLength = maxLength;

    public void ValidateText(string? text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ValidationException(Errors.TopicCannotBeEmpty);
        }

        if (text.Length > _maxLength)
        {
            throw new ValidationException(string.Format(Errors.TopicIsTooLong, _maxLength));
        }
    }
}
