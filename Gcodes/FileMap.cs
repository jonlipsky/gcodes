using Gcodes.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gcodes
{
    /// <summary>
    /// A map for finding details about a particular location in text.
    /// </summary>
    public class FileMap
    {
        private SortedDictionary<int, Location> _locations = new();
        private Dictionary<Span, SpanInfo> _spans = new();
        private string _src;


        public FileMap(string src)
        {
            _src = src ?? throw new ArgumentNullException(nameof(src));
        }

        /// <summary>
        /// Get information associated with the provided span.
        /// </summary>
        /// <param name="span"></param>
        /// <returns></returns>
        public SpanInfo SpanInfoFor(Span span)
        {
            if (!_spans.TryGetValue(span, out SpanInfo info))
            {
                info = _spans[span] = CalculateSpanInfo(span);
            }

            return info;
        }

        private SpanInfo CalculateSpanInfo(Span span)
        {
            var start = LocationFor(span.Start);
            var end = LocationFor(span.End);
            var value = _src.Substring(span.Start, span.Length);

            return new SpanInfo(span, start, end, value);
        }

        public Location LocationFor(int byteIndex)
        {
            if (!_locations.TryGetValue(byteIndex, out Location location))
            {
                location = _locations[byteIndex] = CalculateLocation(byteIndex);
            }

            return location;
        }

        private Location CalculateLocation(int byteIndex)
        {
            var closestLocation = _locations.Values.Where(loc => loc.ByteIndex < byteIndex).LastOrDefault();

            var line = LineNumber(byteIndex, closestLocation);
            var column = ColumnNumber(byteIndex);

            return new Location(byteIndex, line, column);
        }

        internal int ColumnNumber(int byteIndex)
        {
            var lastNewline = _src.LastIndexOf('\n', byteIndex);
            var col = lastNewline < 0 ? byteIndex + 1 : byteIndex - lastNewline;

            return col;
        }

        internal int LineNumber(int byteIndex, Location closest = null)
        {
            var line = NaiveLineNumber(_src, byteIndex, closest?.ByteIndex ?? 0);

            if (closest != null)
            {
                line += closest.Line - 1;
            }

            return line;
        }

        private int NaiveLineNumber(string src, int byteIndex, int startIndex = 0)
        {
            var line = 1;

            for (int index = startIndex; index < byteIndex; index++)
            {
                var c = src[index];
                if (c == '\n')
                {
                    line += 1;
                }
            }

            return line;
        }
    }
}
