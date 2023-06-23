using System;
using Gcodes.Tokens;

namespace Gcodes;

[Serializable]
public class ParseException : GcodeException
{
    public Span Span { get; } = Span.Empty;

    public ParseException()
    {
    }

    public ParseException(
        string message) : base(message)
    {
    }

    public ParseException(
        string message,
        Span span) : base(message)
    {
        Span = span;
    }

    public ParseException(
        string message,
        Exception inner) : base(message, inner)
    {
    }

    protected ParseException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}