using System.Diagnostics.CodeAnalysis;

namespace BlazorApp1.Server.Exceptions;

public class NotAllowedException : Exception
{
    private const string DefaultMessage = "You're not allowed to do this";

    public NotAllowedException(string? message = null) : base(message ?? DefaultMessage) { }

    public static void ThrowIfNull([NotNull] object? param, string? message = null)
    {
        if (param is null)
        {
            throw new NotAllowedException();
        }
    }
}