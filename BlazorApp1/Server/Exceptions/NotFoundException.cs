using System.Diagnostics.CodeAnalysis;

namespace BlazorApp1.Server.Exceptions;

public class NotFoundException : Exception
{
    private const string DefaultMessage = "There is an error in your request. Check it and try again later";

    public NotFoundException(string? message = null) : base(message ?? DefaultMessage) { }

    public static void ThrowIfNull([NotNull] object? param, string? message = null)
    {
        if (param is null)
        {
            throw new NotFoundException();
        }
    }
}