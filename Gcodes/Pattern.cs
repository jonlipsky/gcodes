using Gcodes.Tokens;
using System.Text.RegularExpressions;

namespace Gcodes
{
    internal class Pattern
    {
        Regex _regex;
        TokenKind _kind;

        public Pattern(string pattern, TokenKind kind)
        {
            if (!pattern.StartsWith(@"\G"))
                pattern = @"\G" + pattern;

            _regex = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            _kind = kind;
        }

        public bool TryMatch(string src, int startIndex, out Token tok)
        {
            var match = _regex.Match(src, startIndex);

            if (match.Success)
            {
                var span = new Span(startIndex, startIndex + match.Length);

                if (_kind.HasValue())
                {
                    tok = new Token(span, _kind, match.Value);
                }
                else
                {
                    tok = new Token(span, _kind);
                }
                return true;
            }
            else
            {
                tok = null;
                return false;
            }
        }
    }
}