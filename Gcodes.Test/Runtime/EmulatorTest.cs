using Gcodes.Runtime;
using Xunit;

namespace Gcodes.Test.Runtime
{
    public class EmulatorTest
    {
        [Fact]
        public void CanInstantiate()
        {
            var emulator = new Emulator();
        }

        [Theory(Skip = "Not all operations are implemented")]
        [InlineData("circle.gcode")]
        [InlineData("simple_mill.gcode")]
        [InlineData("371373P.gcode")]
        public void EmulateAValidGcodeProgram(string filename)
        {
            var src = EmbeddedFixture.ExtractFile(filename);
            var emulator = new Emulator();

            emulator.Run(src);
        }
    }
}
