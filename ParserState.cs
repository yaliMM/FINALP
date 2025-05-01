using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpToCpp;

namespace CSharpToCpp.Parser
{
    public enum ParserState
    {
        START,
        STATEMENT,
        IF,
        EXPRESSION,
        CONDITION,
        ERROR
    }
}


