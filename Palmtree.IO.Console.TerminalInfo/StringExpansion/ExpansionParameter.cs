using System;
using System.Text.RegularExpressions;

namespace Palmtree.IO.Console.StringExpansion
{
    internal abstract class ExpansionParameter
    {
        private static readonly Regex _formatSpecPattern;

        static ExpansionParameter()
        {
            _formatSpecPattern = new Regex(@"^(?<width>\d+)?(\.(?<precision>\d+))?(?<type>[cdosuxX])$", RegexOptions.Compiled);
        }

        public abstract Int32 AsNumber();
        public abstract Boolean AsBool();
        public abstract String AsString();

        public String Format(String formatSpec)
        {
            var match = _formatSpecPattern.Match(formatSpec);
            if (!match.Success)
                throw new ExpansionStringSyntaxErrorExceptionException($"Invalid format spec. \"{formatSpec}\"");

            return
                Format(
                    match.Groups["width"].Success ? match.Groups["width"].Value : "",
                    match.Groups["precision"].Success ? match.Groups["precision"].Value : "",
                    match.Groups["type"].Value);
        }

        protected abstract String Format(String width, String precision, String typeSpec);
    }
}
