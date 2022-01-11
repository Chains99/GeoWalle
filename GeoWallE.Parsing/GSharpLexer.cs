using GeoWallE.GraphicRuntime.Core;
using GeoWallE.Parsing.LexicalAnalysis;
using System.Collections.Generic;

namespace GeoWallE.Parsing
{
    public class GSharpLexer
    {
        private static LexicalAnalyzer _LexicalAnalyzer;

        /// <summary>
        /// Lexical analysis. Allows to split a raw text representing the program into the first abstract elements (tokens).<para/>
        /// i.e.
        /// if 2 &lt; 3 then 4 else 5 <para/>
        /// Can be split in tokens keyword[if] number[2] symbol[&lt;] number[3] keyword[then] number[4] keyword[else] number[5]<para/>
        /// After this process is easier to recognize if an expression refers to a conditional because it "starts with [if] keyword".
        /// </summary>
        static GSharpLexer()
        {
            _LexicalAnalyzer = new LexicalAnalyzer();

            #region Keywords

            _LexicalAnalyzer.RegisterKeyword("point", TokenValues.Point);
            _LexicalAnalyzer.RegisterKeyword("ray", TokenValues.Ray);
            _LexicalAnalyzer.RegisterKeyword("segment", TokenValues.Segment);
            _LexicalAnalyzer.RegisterKeyword("line", TokenValues.Line);
            _LexicalAnalyzer.RegisterKeyword("arc", TokenValues.Arc);
            _LexicalAnalyzer.RegisterKeyword("circle", TokenValues.Circle);
            _LexicalAnalyzer.RegisterKeyword("sequence", TokenValues.Sequence);

            _LexicalAnalyzer.RegisterKeyword("if", TokenValues.If);
            _LexicalAnalyzer.RegisterKeyword("then", TokenValues.Then);
            _LexicalAnalyzer.RegisterKeyword("else", TokenValues.Else);
            _LexicalAnalyzer.RegisterKeyword("let", TokenValues.Let);
            _LexicalAnalyzer.RegisterKeyword("in", TokenValues.In);
            _LexicalAnalyzer.RegisterKeyword("and", TokenValues.And);
            _LexicalAnalyzer.RegisterKeyword("or", TokenValues.Or);
            _LexicalAnalyzer.RegisterKeyword("not", TokenValues.Not);

            _LexicalAnalyzer.RegisterKeyword("import", TokenValues.Import);
            _LexicalAnalyzer.RegisterKeyword("draw", TokenValues.Draw);
            _LexicalAnalyzer.RegisterKeyword("color", TokenValues.Color);
            _LexicalAnalyzer.RegisterKeyword("restore", TokenValues.Restore);
            _LexicalAnalyzer.RegisterKeyword("print", TokenValues.Print);

            _LexicalAnalyzer.RegisterKeyword("blue", TokenValues.Blue);
            _LexicalAnalyzer.RegisterKeyword("red", TokenValues.Red);
            _LexicalAnalyzer.RegisterKeyword("cyan", TokenValues.Cyan);
            _LexicalAnalyzer.RegisterKeyword("yellow", TokenValues.Yellow);
            _LexicalAnalyzer.RegisterKeyword("green", TokenValues.Green);
            _LexicalAnalyzer.RegisterKeyword("magenta", TokenValues.Magenta);
            _LexicalAnalyzer.RegisterKeyword("white", TokenValues.White);
            _LexicalAnalyzer.RegisterKeyword("black", TokenValues.Black);
            _LexicalAnalyzer.RegisterKeyword("gray", TokenValues.Gray);

            _LexicalAnalyzer.RegisterKeyword("_", TokenValues._);

            _LexicalAnalyzer.RegisterKeyword("undefined", TokenValues.Undefined);

            #endregion

            #region Operators

            _LexicalAnalyzer.RegisterOperator("+", TokenValues.Add);
            _LexicalAnalyzer.RegisterOperator("*", TokenValues.Mul);
            _LexicalAnalyzer.RegisterOperator("-", TokenValues.Sub);
            _LexicalAnalyzer.RegisterOperator("/", TokenValues.Div);
            _LexicalAnalyzer.RegisterOperator("%", TokenValues.Mod);
            _LexicalAnalyzer.RegisterOperator("<", TokenValues.Less);
            _LexicalAnalyzer.RegisterOperator("<=", TokenValues.LessOrEquals);
            _LexicalAnalyzer.RegisterOperator(">", TokenValues.Greater);
            _LexicalAnalyzer.RegisterOperator(">=", TokenValues.GreaterOrEquals);
            _LexicalAnalyzer.RegisterOperator("==", TokenValues.Equals);
            _LexicalAnalyzer.RegisterOperator("!=", TokenValues.NotEquals);
            _LexicalAnalyzer.RegisterOperator("=", TokenValues.Assign);

            _LexicalAnalyzer.RegisterOperator(",", TokenValues.ValueSeparator);
            _LexicalAnalyzer.RegisterOperator(";", TokenValues.StatementSeparator);
            _LexicalAnalyzer.RegisterOperator("(", TokenValues.OpenBracket);
            _LexicalAnalyzer.RegisterOperator(")", TokenValues.ClosedBracket);
            _LexicalAnalyzer.RegisterOperator("{", TokenValues.OpenCurlyBraces);
            _LexicalAnalyzer.RegisterOperator("}", TokenValues.ClosedCurlyBraces);
            _LexicalAnalyzer.RegisterOperator("...", TokenValues.Dots);

            #endregion

            #region Comments

            _LexicalAnalyzer.RegisterComment("//", "\n");
            _LexicalAnalyzer.RegisterComment("/*", "*/");

            #endregion

            #region Texts

            _LexicalAnalyzer.RegisterText("\"", "\"", false);

            #endregion
        }

        public static LexicalAnalyzer LexicalAnalyzer
        {
            get { return _LexicalAnalyzer; }
        }

        private readonly OutputInfo _outputInfo;

        public GSharpLexer(OutputInfo outputInfo)
        {
            _outputInfo = outputInfo;
        }

        public TokenConsumer GetTokenConsumer(string fileName, string code)
        {
            var errors = new List<CompilingError>();

            var tokens = _LexicalAnalyzer.GetTokens(fileName, code, errors);
            foreach (var error in errors)
            {
                _outputInfo.AddError(error);
            }

            return new TokenConsumer(tokens);
        }
    }
}
