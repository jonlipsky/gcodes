using Gcodes.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gcodes;

/// <summary>
/// A tokenizer for converting a stream of characters into a stream of
/// <see cref="Token"/>s.
/// </summary>
public class Lexer
{
    private readonly List<Pattern> _patterns;
    private readonly List<Regex> _skips;
    private readonly string _src;
    private int _pointer;
    private int _lineNumber;

    private bool Finished => _pointer >= _src.Length;

    /// <summary>
    /// Event fired whenever a comment is encountered.
    /// </summary>
    public event EventHandler<CommentEventArgs>? CommentDetected;

    /// <summary>
    /// Create a new <see cref="Lexer"/> which will tokenize the provided 
    /// source text.
    /// </summary>
    /// <param name="src"></param>
    public Lexer(string src)
    {
        _skips = new List<Regex>
        {
            new(@"\G\s+", RegexOptions.Compiled),
            new(@"\G;([^\n\r]*)", RegexOptions.Compiled),
            new(@"\G\(([^)\n\r]*)\)", RegexOptions.Compiled)
        };
        _src = src;
        _pointer = 0;
        _lineNumber = 0;

        _patterns = new List<Pattern>
        {
            new(@"G", TokenKind.G),
            new(@"O", TokenKind.O),
            new(@"N", TokenKind.N),
            new(@"M", TokenKind.M),
            new(@"T", TokenKind.T),
            new(@"X", TokenKind.X),
            new(@"Y", TokenKind.Y),
            new(@"Z", TokenKind.Z),
            new(@"F", TokenKind.F),
            new(@"I", TokenKind.I),
            new(@"J", TokenKind.J),
            new(@"K", TokenKind.K),
            new(@"A", TokenKind.A),
            new(@"B", TokenKind.B),
            new(@"C", TokenKind.C),
            new(@"H", TokenKind.H),
            new(@"P", TokenKind.P),
            new(@"S", TokenKind.S),

            new(@"[-+]?(\d+\.\d+|\.\d+|\d+\.?)", TokenKind.Number)
        };
    }

    /// <summary>
    /// Start tokenizing the input.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Token> Tokenize()
    {
        while (!Finished)
        {
            SkipStuff();
            if (Finished) break;
            yield return NextToken();
        }
    }

    private void SkipStuff()
    {
        int currentPass;

        do
        {
            currentPass = _pointer;

            foreach (var skip in _skips)
            {
                var match = skip.Match(_src, _pointer);

                if (match.Success)
                {
                    OnCommentDetected(match);
                    _pointer += match.Length;
                    _lineNumber += match.Value.Count(c => c == '\n');
                }
            }
        } while (_pointer < _src.Length && _pointer != currentPass);
    }

    private void OnCommentDetected(Match match)
    {
        for (var i = 1; i < match.Groups.Count; i++)
        {
            var group = match.Groups[i];
            if (group.Success)
            {
                var span = new Span(_pointer, _pointer + match.Length);
                CommentDetected?.Invoke(this, new CommentEventArgs(group.Value, span));
                break;
            }
        }
    }

    private Token NextToken()
    {
        foreach (var pat in _patterns)
        {
            if (pat.TryMatch(_src, _pointer, out var tok))
            {
                _pointer = tok!.Span.End;
                if (tok.Value != null)
                {
                    _lineNumber += tok.Value.Count(c => c == '\n');
                }

                return tok;
            }
        }

        var column = CurrentColumn();
        throw new UnrecognisedCharacterException(_lineNumber + 1, column + 1, _src[_pointer]);
    }

    private int CurrentColumn()
    {
        var lastNewline = _src.LastIndexOf('\n', _pointer);
        return lastNewline < 0 ? _pointer : _pointer - lastNewline;
    }
}