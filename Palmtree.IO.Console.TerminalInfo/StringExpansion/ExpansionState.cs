using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Palmtree.IO.Console.StringExpansion
{
    internal class ExpansionState
    {
        private class ArgumentsAccesser
            : IArgumentIndexer<Char, ExpansionParameter>
        {
            private readonly Char _startIndex;
            private readonly Char _endIndex;
            private readonly ExpansionParameter[] _parameters;

            public ArgumentsAccesser(Object[] parameters, Char startIndex, Char endIndex)
            {
                if (startIndex > endIndex)
                    throw new ArgumentException($"{nameof(startIndex)} > {nameof(endIndex)}");

                if (parameters.Length > endIndex - startIndex + 1)
                    throw new ExpansionBadArgumentExceptionException($"Too many parameters.: {parameters.Length}");

                _startIndex = startIndex;
                _endIndex = endIndex;
                _parameters =
                    parameters
                    .Select(arg =>
                        arg switch
                        {
                            Int32 intArg => new ExpansionNumberParameter(intArg) as ExpansionParameter,
                            Char charArg => new ExpansionNumberParameter(charArg),
                            Boolean boolArg => new ExpansionNumberParameter(boolArg),
                            String stringArgument => new ExpansionStringParameter(stringArgument),
                            _ => throw new ExpansionBadArgumentExceptionException($"Not supported argument type.: arg=\"{arg}\", type={arg.GetType().FullName}"),
                        })
                    .ToArray();
            }

            public ExpansionParameter this[Char index]
            {
                get
                {
                    if (!index.IsBetween(_startIndex, _endIndex) || index - _startIndex >= _parameters.Length)
                        throw new ExpansionBadArgumentExceptionException($"Failed to get argument '{index}'.");

                    return _parameters[index - _startIndex];
                }

                set
                {
                    if (!index.IsBetween(_startIndex, _endIndex) || index - _startIndex >= _parameters.Length)
                        throw new ExpansionBadArgumentExceptionException($"Failed to set argument '{index}'.");

                    _parameters[index - _startIndex] = value;
                }
            }

            public Boolean TryGet(Char index, [NotNullWhen(true)] out ExpansionParameter? value)
            {
                if (!index.IsBetween(_startIndex, _endIndex) || index - _startIndex >= _parameters.Length)
                {
                    value = null;
                    return false;
                }
                else
                {
                    value = _parameters[index - _startIndex];
                    return true;
                }
            }
        }

        private class VariableAccesser
            : IIndexer<Char, ExpansionParameter>
        {
            private readonly Char _startIndex;
            private readonly Char _endIndex;
            private readonly ExpansionParameter[] _values;

            public VariableAccesser(Char startIndex, Char endIndex)
            {
                if (startIndex > endIndex)
                    throw new ArgumentException($"{nameof(startIndex)} > {nameof(startIndex)}");

                _startIndex = startIndex;
                _endIndex = endIndex;
                _values = new ExpansionParameter[_endIndex - startIndex + 1];
            }

            public ExpansionParameter this[Char index]
            {
                get
                {
                    if (!index.IsBetween(_startIndex, _endIndex))
                        throw new ExpansionStringSyntaxErrorExceptionException($"'{index}' is an invalid variable name.");

                    return
                        _values[index - _startIndex]
                        ?? throw new ExpansionStringSyntaxErrorExceptionException($"Failed to get variable '{index}'.");
                }

                set
                {
                    if (!index.IsBetween(_startIndex, _endIndex))
                        throw new ExpansionStringSyntaxErrorExceptionException($"'{index}' is an invalid variable name.");
                    if (value is null)
                        throw new InvalidOperationException($"Must not set null in '{index}'.");

                    _values[index - _startIndex] = value;
                }
            }
        }

        private readonly ExpansionReader _reader;
        private readonly Stack<ExpansionParameter> _stack;

        public ExpansionState(String sourceString, Object[] args)
        {
            SourceString = sourceString;
            _reader = new ExpansionReader(sourceString);
            Arguments = new ArgumentsAccesser(args, '1', '9');
            _stack = new Stack<ExpansionParameter>();
            DynamicValues = new VariableAccesser('a', 'z');
            StaticValues = new VariableAccesser('A', 'Z');
        }

        public String SourceString { get; }
        public IArgumentIndexer<Char, ExpansionParameter> Arguments { get; }
        public IIndexer<Char, ExpansionParameter> DynamicValues { get; }
        public IIndexer<Char, ExpansionParameter> StaticValues { get; }
        public Char? ReadCharOrNull() => _reader.Read();

        public Char ReadChar()
            => _reader.Read()
                ?? throw new InvalidOperationException("Unexpected end of string.");

        public Boolean ReaderStartsWith(String s) => _reader.StartsWith(s);
        public void Push(ExpansionParameter value) => _stack.Push(value);
        public ExpansionParameter Pop() => _stack.Pop();
    }
}
