using System;
using System.Collections.Generic;
using CSharpToCpp.Common;

namespace CSharpToCpp.Parser
{
   


    public class Parser
    {
        private List<Token> _tokens;
        private int _position;
        private ParserState _state;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _position = 0;
            _state = ParserState.START;
        }

        public void Start()
        {
            while (_position < _tokens.Count)
            {
                switch (_state)
                {
                    case ParserState.START:
                        HandleStart();
                        break;
                    case ParserState.STATEMENT:
                        HandleStatement();
                        break;
                    case ParserState.CONDITION:
                        HandleCondition();
                        break;
                    case ParserState.ERROR:
                        Console.WriteLine("❌ Parsing error near token: " + _tokens[_position].Value);
                        _position++;
                        _state = ParserState.START;
                        break;
                }
            }
        }

        private void HandleStart()
        {
            var token = Peek(0);

            if (token.Type == TokenType.Type || token.Type == TokenType.Keyword || token.Type == TokenType.Identifier)
                _state = ParserState.STATEMENT;
            else
                _state = ParserState.ERROR;
        }

        private void HandleStatement()
        {
            var current = Peek(0);

            if (current.Type == TokenType.Type)
            {
                // דילוג על הצהרת משתנה: int x = 5;
                _position++; // type
                if (Peek(0).Type == TokenType.Identifier) _position++;
                if (Peek(0).Value == "=") _position++;
                if (Peek(0).Type == TokenType.Number || Peek(0).Type == TokenType.String || Peek(0).Type == TokenType.Char) _position++;
                if (Peek(0).Value == ";") _position++;
            }
            else if (current.Value == "if")
            {
                _state = ParserState.CONDITION;
                return;
            }
            else
            {
                _state = ParserState.ERROR;
                return;
            }

            _state = ParserState.START;
        }

        private void HandleCondition()
        {
            // תנאי if (...) { ... }
            _position++; // if
            if (Peek(0).Value == "(") _position++;
            if (Peek(0).Type == TokenType.Identifier) _position++;
            if (Peek(0).Type == TokenType.Operator) _position++;
            if (Peek(0).Type == TokenType.Identifier || Peek(0).Type == TokenType.Number) _position++;
            if (Peek(0).Value == ")") _position++;

            if (Peek(0).Value == "{")
            {
                int braceCount = 1;
                _position++;
                while (_position < _tokens.Count && braceCount > 0)
                {
                    if (Peek(0).Value == "{") braceCount++;
                    if (Peek(0).Value == "}") braceCount--;
                    _position++;
                }
            }

            _state = ParserState.START;
        }

        private Token Peek(int offset)
        {
            return (_position + offset < _tokens.Count) ? _tokens[_position + offset] : new Token(TokenType.Unknown, "");
        }
    }
}
