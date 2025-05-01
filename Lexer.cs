using System;
using System.Collections.Generic;
using System.Linq;
using CSharpToCpp.Common;

namespace CSharpToCpp.Lexer
{
    public class Lexer
    {
        private readonly string _input;
        private int _position;

        public List<Token> SymbolTable { get; } = new List<Token>();

        public Lexer(string input)
        {
            _input = input;
        }

        public void Start()
        {
            while (_position < _input.Length)
                HandleInit();
        }

        private char PeekChar() =>
            _position < _input.Length ? _input[_position] : '\0';

        private void HandleInit()
        {
            char c = PeekChar();

            // רווחים
            if (char.IsWhiteSpace(c))
            {
                _position++;
                return;
            }

            // מחרוזת
            if (c == '"')
            {
                ReadStringLiteral();
                return;
            }

            // תו בודד
            if (c == '\'')
            {
                ReadCharLiteral();
                return;
            }

            // מספר / float / double
            if (char.IsDigit(c))
            {
                ReadNumberOrFloat();
                return;
            }

            // מזהה או מילה שמורה
            if (char.IsLetter(c) || c == '_')
            {
                ReadIdentifierOrKeyword();
                return;
            }

            // אופרטור (מעדיף ארוך → קצר)
            var rest = _input.Substring(_position);
            var op = LanguageRules.Operators
                        .OrderByDescending(o => o.Length)
                        .FirstOrDefault(o => rest.StartsWith(o));
            if (op != null)
            {
                SymbolTable.Add(new Token(TokenType.Operator, op));
                _position += op.Length;
                return;
            }

            // פיסוק
            if (LanguageRules.Punctuations.Contains(c.ToString()))
            {
                SymbolTable.Add(new Token(TokenType.Punctuation, c.ToString()));
                _position++;
                return;
            }

            // לא מזוהה
            SymbolTable.Add(new Token(TokenType.Unknown, c.ToString()));
            _position++;
        }

        private void ReadIdentifierOrKeyword()
        {
            int start = _position;
            while (_position < _input.Length &&
                   (char.IsLetterOrDigit(_input[_position]) || _input[_position] == '_'))
            {
                _position++;
            }

            string lit = _input.Substring(start, _position - start);
            if (LanguageRules.ReservedWords.TryGetValue(lit, out var type))
                SymbolTable.Add(new Token(type, lit));
            else
                SymbolTable.Add(new Token(TokenType.Identifier, lit));
        }

        private void ReadStringLiteral()
        {
            _position++; // עובר מעל "
            int start = _position;
            while (_position < _input.Length && _input[_position] != '"')
                _position++;
            string content = _input.Substring(start, _position - start);
            SymbolTable.Add(new Token(TokenType.String, $"\"{content}\""));
            if (_position < _input.Length) _position++; // סוגר "
        }

        private void ReadCharLiteral()
        {
            _position++; // עובר מעל '
            if (_position >= _input.Length) return;

            char value = _input[_position++];
            if (_position < _input.Length && _input[_position] == '\'')
            {
                _position++; // סוגר '
                SymbolTable.Add(new Token(TokenType.Char, $"'{value}'"));
            }
            else
            {
                // שגיאה בפיסוק
                SymbolTable.Add(new Token(TokenType.Unknown, value.ToString()));
            }
        }

        private void ReadNumberOrFloat()
        {
            int start = _position;
            // חלק שלם
            while (_position < _input.Length && char.IsDigit(_input[_position]))
                _position++;

            bool sawDot = false;
            // חלק עשרוני?
            if (_position < _input.Length &&
                _input[_position] == '.' &&
                _position + 1 < _input.Length &&
                char.IsDigit(_input[_position + 1]))
            {
                sawDot = true;
                _position++; // ה־'.'
                while (_position < _input.Length && char.IsDigit(_input[_position]))
                    _position++;
            }

            bool isFloat = false;
            // סיומת f או F
            if (_position < _input.Length &&
                (_input[_position] == 'f' || _input[_position] == 'F'))
            {
                isFloat = true;
                _position++;
            }

            string lit = _input.Substring(start, _position - start);
            TokenType type = isFloat
                            ? TokenType.Float
                            : sawDot
                                ? TokenType.Double
                                : TokenType.Number;
            SymbolTable.Add(new Token(type, lit));
        }
    }
}
