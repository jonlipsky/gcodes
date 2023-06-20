using Gcodes.Tokens;
using System;

namespace Gcodes.Ast
{
    /// <summary>
    /// A "machine" code, typically used for invoking special machine-specific
    /// subroutines or actions.
    /// </summary>
    public class Mcode : Code, IEquatable<Mcode>
    {
        public Mcode(int number, Span span, int? line = null): base(span, line)
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
            return Equals(obj as Mcode);
        }

        public bool Equals(Mcode? other)
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

        public static bool operator ==(Mcode? mcode1, Mcode? mcode2)
        {
            return mcode1?.Equals(mcode2) ?? false;
        }

        public static bool operator !=(Mcode mcode1, Mcode mcode2)
        {
            return !(mcode1 == mcode2);
        } 
        #endregion
    }
}