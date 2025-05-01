using System;
using System.Diagnostics;
using System.IO;

namespace CSharpToCpp.CodeGenerator
{
    public enum GeneratorState
    {
        START,
        STATEMENT,
        IF_CONDITION,
        PRINT,
        ERROR
    }
}

