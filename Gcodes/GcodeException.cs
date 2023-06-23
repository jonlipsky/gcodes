using System;

namespace Gcodes;

[Serializable]
public class GcodeException : Exception
{
    public GcodeException()
    {
    }

    public GcodeException(
        string message) : base(message)
    {
    }

    public GcodeException(
        string message,
        Exception inner) : base(message, inner)
    {
    }

    protected GcodeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}