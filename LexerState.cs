using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpToCpp;



namespace CSharpToCpp.Lexer
{
    public enum LexerState
    {
        INIT,
        IDENTIFIER,
        NUMBER,
        STRING,
        OPERATOR,
        CHAR,
        ERROR
    }
}

