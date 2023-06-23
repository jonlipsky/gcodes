using System;
using Gcodes.Ast;

namespace Gcodes.Tokens;

/// <summary>
/// Extension methods for the <see cref="Token"/> enum.
/// </summary>
public static class TokenKindExt
{
    /// <summary>
    /// Does this <see cref="TokenKind"/> have a meaningful string value?
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    public static bool HasValue(this TokenKind kind)
    {
        return kind switch
        {
            TokenKind.Number => true,
            _ => false
        };
    }

    /// <summary>
    /// Try to convert the <see cref="TokenKind"/> into an 
    /// <see cref="ArgumentKind"/>, if applicable.
    /// </summary>
    /// <param name="kind"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Raised if there is no corresponding <see cref="ArgumentKind"/> for
    /// this <see cref="TokenKind"/>.
    /// </exception>
    public static ArgumentKind AsArgumentKind(this TokenKind kind)
    {
        return kind switch
        {
            TokenKind.X => ArgumentKind.X,
            TokenKind.Y => ArgumentKind.Y,
            TokenKind.Z => ArgumentKind.Z,
            TokenKind.A => ArgumentKind.A,
            TokenKind.B => ArgumentKind.B,
            TokenKind.C => ArgumentKind.C,
            TokenKind.I => ArgumentKind.I,
            TokenKind.J => ArgumentKind.J,
            TokenKind.K => ArgumentKind.K,
            TokenKind.H => ArgumentKind.H,
            TokenKind.P => ArgumentKind.P,
            TokenKind.S => ArgumentKind.S,
            TokenKind.F => ArgumentKind.FeedRate,
            _ => throw new ArgumentOutOfRangeException(nameof(kind), "No equivalent argument kind")
        };
    }
}