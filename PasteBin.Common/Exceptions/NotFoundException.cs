namespace PasteBin.Common.Exceptions;
public class NotFoundException(string? message) : Exception(message)
{
}