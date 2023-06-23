using System;

namespace Gcodes;

[Serializable]
public class UnrecognisedCharacterException : GcodeException
{
    public int Line { get; }
    public int Column { get; }
    public char Character { get; }

    public UnrecognisedCharacterException(int line, int column, char character)
        : this($"Unrecognised character \"{character}\" at line {line} column {column}")
    {
        Line = line;
        Column = column;
        Character = character;
    }

    public UnrecognisedCharacterException()
    {
    }

    public UnrecognisedCharacterException(string message) : base(message)
    {
    }

    public UnrecognisedCharacterException(string message, Exception inner) : base(message, inner)
    {
    }

    protected UnrecognisedCharacterException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }
}