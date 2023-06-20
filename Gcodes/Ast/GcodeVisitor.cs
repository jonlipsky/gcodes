namespace Gcodes.Ast
{
    /// <summary>
    /// An object which can be used to inspect gcodes at runtime using
    /// the visitor pattern.
    /// </summary>
    public interface IGcodeVisitor
    {
        void Visit(Gcode code);
        void Visit(Mcode code);
        void Visit(Tcode code);
        void Visit(Ocode code);
    }
}
