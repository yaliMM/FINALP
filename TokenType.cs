using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpToCpp.Common;

namespace CSharpToCpp.Common
{
    public enum TokenType
    {
        Keyword,
        Type,
        Identifier,
        Number,
        Float,
        Double,
        Operator,
        Punctuation,
        String,
        Char,
        Unknown
    }
}
