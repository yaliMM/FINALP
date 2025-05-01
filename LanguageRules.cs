using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpToCpp.Common;


namespace CSharpToCpp.Common
{
    public static class LanguageRules
    {
        // which words come out as TokenType.Keyword vs. TokenType.Type
        public static readonly Dictionary<string, TokenType> ReservedWords = new Dictionary<string, TokenType>()
        {
            // control-flow
            { "if",      TokenType.Keyword },
            { "else",    TokenType.Keyword },
            { "for",     TokenType.Keyword },
            { "foreach", TokenType.Keyword },
            { "while",   TokenType.Keyword },

            // built-in types
            { "int",     TokenType.Type },
            { "long",    TokenType.Type },
            { "float",   TokenType.Type },
            { "double",  TokenType.Type },
            { "char",    TokenType.Type },
            { "string",  TokenType.Type },
            { "bool",    TokenType.Type },
        };

        public static readonly HashSet<string> Operators = new HashSet<string>()
        {
            "+",  "-",  "*",  "/",  "%",
            "=",  "==", "!=", "<",  ">",
            "<=", ">=", "++", "--",
            "+=", "-=", "*=", "/="
        };

        public static readonly HashSet<string> Punctuations = new HashSet<string>()
        {
            "(", ")", "{", "}", ";", ",", ".", "[", "]"
        };
    }
}
