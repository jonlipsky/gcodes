using System;
using Gcodes.Tokens;

namespace Gcodes;

[Serializable]
public class UnexpectedTokenException : ParseException
{
    public TokenKind[]? Expected { get; }
    public TokenKind Found { get; }

    public UnexpectedTokenException(TokenKind[] expected, TokenKind found, Span span)
        : base($"Expected one of [{string.Join(", ", expected)}] but found {found}", span)
    {
        Expected = expected;
        Found = found;
    }

    public UnexpectedTokenException()
    {
    }

    public UnexpectedTokenException(string message) : base(message)
    {
    }

    public UnexpectedTokenException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UnexpectedTokenException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}