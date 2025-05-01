using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpToCpp.Common;

namespace CSharpToCpp.CodeGenerator
{
    public class CodeGenerator
    {
        private readonly List<Token> _tokens;

        public CodeGenerator(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public string Generate()
        {
            var sb = new StringBuilder();

            // ---------- includes ----------
            sb.AppendLine("#include <iostream>");
            if (_tokens.Any(t => t.Type == TokenType.String))
                sb.AppendLine("#include <string>");
            sb.AppendLine("using namespace std;");
            sb.AppendLine();

            // ---------- main header ----------
            sb.AppendLine("int main() {");

            int indent = 4;
            bool needIndent = true;

            for (int i = 0; i < _tokens.Count; i++)
            {
                var token = _tokens[i];

                // אם צריך זחיחה בתחילת פקודה
                if (needIndent)
                {
                    sb.Append(' ', indent);
                    needIndent = false;
                }

                // טיפול מיוחד ב־Console.WriteLine(...)
                if (token.Type == TokenType.Identifier && token.Value == "Console"
                    && i + 3 < _tokens.Count
                    && _tokens[i + 1].Type == TokenType.Punctuation && _tokens[i + 1].Value == "."
                    && _tokens[i + 2].Type == TokenType.Identifier && _tokens[i + 2].Value == "WriteLine"
                    && _tokens[i + 3].Type == TokenType.Punctuation && _tokens[i + 3].Value == "(")
                {
                    // דילוג על Console . WriteLine (
                    i += 4;

                    // קוראים את כל הטוקנים עד הסוגרית המתאימה
                    var argsSb = new StringBuilder();
                    int depth = 1;
                    while (i < _tokens.Count && depth > 0)
                    {
                        var t = _tokens[i];
                        if (t.Type == TokenType.Punctuation)
                        {
                            if (t.Value == "(") depth++;
                            else if (t.Value == ")") depth--;
                        }

                        if (depth > 0)
                            argsSb.Append(t.Value);

                        i++;
                    }

                    // עכשיו argsSb מכיל למשל:  "\"Age: \" + age"
                    string raw = argsSb.ToString().Trim();

                    // מפצלים על '+' ומדביקים ב־<<
                    var parts = raw
                        .Split(new[] { '+' }, System.StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim());

                    sb.Append("cout << ")
                      .Append(string.Join(" << ", parts))
                      .Append(" << endl;")
                      .AppendLine();

                    needIndent = true;
                    continue;
                }

                // טיפול בשאר הטוקנים
                switch (token.Type)
                {
                    case TokenType.Punctuation when token.Value == ";":
                        sb.Append(";").AppendLine();
                        needIndent = true;
                        break;

                    case TokenType.Punctuation when token.Value == "{":
                        sb.Append(" {").AppendLine();
                        indent += 4;
                        needIndent = true;
                        break;

                    case TokenType.Punctuation when token.Value == "}":
                        indent -= 4;
                        sb.Append(' ', indent)
                          .Append("}").AppendLine();
                        needIndent = true;
                        break;

                    case TokenType.Type:
                        sb.Append(token.Value).Append(' ');
                        break;

                    case TokenType.Operator:
                        sb.Append(' ')
                          .Append(token.Value)
                          .Append(' ');
                        break;

                    case TokenType.Punctuation:
                        sb.Append(token.Value);
                        break;

                    case TokenType.Identifier:
                    case TokenType.Number:
                    case TokenType.Float:
                    case TokenType.Double:
                    case TokenType.Char:
                    case TokenType.String:
                    case TokenType.Keyword:
                        sb.Append(token.Value);
                        break;

                    default:
                        sb.Append(token.Value);
                        break;
                }
            }

            // return בסיום
            sb.AppendLine();
            sb.AppendLine("    return 0;");
            sb.Append("}");

            return sb.ToString();
        }
    }
}
