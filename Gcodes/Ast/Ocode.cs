using Gcodes.Tokens;

namespace Gcodes.Ast;

public class Ocode : Code
{
    public Ocode(int programNumber, Span span, int? line = null) : base(span, line)
    {
        ProgramNumber = programNumber;
    }

    public int ProgramNumber { get; }

    public override void Accept(IGcodeVisitor visitor)
    {
        visitor.Visit(this);
    }
}