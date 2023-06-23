using System;

namespace Gcodes;

[Serializable]
public class UnknownGcodeException : GcodeException
{
    public UnknownGcodeException()
    {
    }

    public UnknownGcodeException(string message) : base(message)
    {
    }

    public UnknownGcodeException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UnknownGcodeException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}