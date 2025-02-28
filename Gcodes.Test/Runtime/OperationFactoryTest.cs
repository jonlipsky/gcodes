﻿using Gcodes.Ast;
using Gcodes.Runtime;
using Gcodes.Tokens;
using System.Collections.Generic;
using Xunit;

namespace Gcodes.Test.Runtime
{
    public class OperationFactoryTest
    {
        OperationFactory operations = new OperationFactory();
        MachineState initialState = new MachineState();

        [Fact]
        public void GetIgnoredInstruction()
        {
            const int number = 1;
            operations.IgnoreGcode(number);

            var got = operations.GcodeOp(new Gcode(number, new List<Argument>(), Span.Empty), initialState);

            Assert.IsType<Noop>(got);
        }

        [Fact]
        public void RecogniseADwell()
        {
            var duration = 5;
            var code = new Gcode(4, new List<Argument> { new Argument(ArgumentKind.P, duration * 1000, Span.Empty) }, Span.Empty);
            var shouldBe = new Noop(initialState, duration);

            var got = operations.GcodeOp(code, initialState);

            Assert.Equal(shouldBe, got);
        }
    }
}
