using System;
using Gcodes.Tokens;

namespace Gcodes;

[Serializable]
public class UnexpectedEofException : ParseException
{
    public TokenKind[]? Expected { get; }

    public UnexpectedEofException(TokenKind[] expected)
        : this($"Expected one of [{string.Join(", ", expected)}] but reached the end of input")
    {
        Expected = expected;
    }

    public UnexpectedEofException()
    {
    }

    public UnexpectedEofException(string message) : base(message)
    {
    }

    public UnexpectedEofException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UnexpectedEofException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}