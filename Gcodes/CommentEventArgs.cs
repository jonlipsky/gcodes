using System;
using Gcodes.Tokens;

namespace Gcodes;

/// <summary>
/// The event arguments passed in when the <see cref="Lexer.CommentDetected"/>
/// event is fired.
/// </summary>
public class CommentEventArgs : EventArgs
{
    internal CommentEventArgs(string comment, Span span)
    {
        Comment = comment ?? throw new ArgumentNullException(nameof(comment));
        Span = span;
    }

    /// <summary>
    /// The comment's contents.
    /// </summary>
    public string Comment { get; }

    /// <summary>
    /// The location of the comment in the source text.
    /// </summary>
    public Span Span { get; }
}