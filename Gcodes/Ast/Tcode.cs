using System;
using Gcodes.Tokens;

namespace Gcodes.Ast
{
    /// <summary>
    /// An tool change instruction.
    /// </summary>
    public class Tcode : Code, IEquatable<Tcode>
    {
        public Tcode(int number, Span span, int? line = null) : base(span, line)
        {
            Number = number;
        }

        public int Number { get; }

        public override void Accept(IGcodeVisitor visitor)
        {
            visitor.Visit(this);
        }

        #region Equals
        public override bool Equals(object? obj)
        {
            return Equals(obj as Tcode);
        }

        public bool Equals(Tcode? other)
        {
            return !ReferenceEquals(other, null) &&
                   base.Equals(other) &&
                   Number == other.Number;
        }

        public override int GetHashCode()
        {
            var hashCode = -2028225194;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Tcode? tcode1, Tcode? tcode2)
        {
            return tcode1?.Equals(tcode2) ?? false;
        }

        public static bool operator !=(Tcode? tcode1, Tcode? tcode2)
        {
            return !(tcode1 == tcode2);
        } 
        #endregion
    }
}
