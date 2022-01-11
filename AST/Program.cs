using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Core;
using GeoWallE.Parsing;

namespace AST
{
    public class program : Node
    {
        public static GDrawer drawer;
        public static OutputInfo errors;
        public static RichTextBox output;
        public static GSharpLexer lexer;
        public static int maxX;
        public static int maxY;
        public static int padding = 30;

        List<Import> imports;
        SeqExpresion exp;

        public override bool build(TokenConsumer context)
        {
            if (context.EndOfTokens)
                return true;
            imports = new List<Import>();

            while (true)
            {
                Import imp = new Import();
                if (!Verify(context, imp))
                    break;
                imports.Add(imp);
            }
            if (context.EndOfTokens)
                return true;
            this.exp = new SeqExpresion();
            return this.exp.build(context);
        }
        public void EvalProgram(Scope scope)
        {
            setFunctions(scope);
            this.eval(scope);
        }

        public override GSharpObject eval(Scope scope)
        {
            foreach (var item in imports)
                item.eval(scope);

            if(this.exp != null)
                return this.exp.eval(scope);
            return null;
        }
        void setFunctions(Scope scope)
        {
            var circle = new FuncCircle();
            scope.setFunc(circle.funcName, circle);

            var ray = new FuncRay();
            scope.setFunc(ray.funcName, ray);

            var line = new FuncLine();
            scope.setFunc(line.funcName, line);

            var segment = new FuncSegment();
            scope.setFunc(segment.funcName, segment);

            var measure = new FuncMeasure();
            scope.setFunc(measure.funcName, measure);

            var intersect = new FuncIntersect();
            scope.setFunc(intersect.funcName, intersect);

            var point = new FuncPoint();
            scope.setFunc(point.funcName, point);

            var count = new FuncCount();
            scope.setFunc(count.funcName, count);

            var randoms = new FuncRandoms();
            scope.setFunc(randoms.funcName, randoms);

            var samples = new FuncSamples();
            scope.setFunc(samples.funcName, samples);

            var arc = new FuncArc();
            scope.setFunc(arc.funcName, arc);
        }
        private bool Verify(TokenConsumer context, Import import)
        {

            int pos = context.getPosition();
            if (!import.build(context))
            {
                context.setPosition(pos);
                return false;
            }
            return true;
        }


        public static TokenConsumer getNextExpTokens(TokenConsumer c, string tokenValue, string secondaryTokenValue)
        {

            var tokens = new List<Token>();
            int opens = 0;
            int seqOpens = 0;
            int letOpens = 0;
            int ifOpen = 0;

            var ifToken = c.Current;
            //si el token q se busca no se encuentra, entonces devolver al tokenConsumer a la posicion original q tenia.
            var pos = c.getPosition();
            tokens.Add(c.Current);
            do
            {
                if (c.Current.Value == TokenValues.OpenBracket)
                    opens++;
                else if (c.Current.Value == TokenValues.ClosedBracket)
                    opens--;
                else if (c.Current.Value == TokenValues.OpenCurlyBraces)
                    seqOpens++;
                else if (c.Current.Value == TokenValues.ClosedCurlyBraces)
                    seqOpens--;
                else if (c.Current.Value == TokenValues.Let)
                    letOpens++;
                else if (c.Current.Value == TokenValues.In)
                    letOpens--;
                else if (c.Current.Value == TokenValues.If)
                {
                    if (ifOpen == 0)
                        ifToken = c.Current;
                    ifOpen++;
                }
                else if (c.Current.Value == TokenValues.Else)
                    ifOpen--;

                if (!c.Next())
                {
                    if(opens>0)
                        program.errors.AddError(new CompilingError(c.CurrentPrev.Location, ErrorCode.Expected, "Parentesis no balanceados."));
                    if(seqOpens >0)
                        program.errors.AddError(new CompilingError(c.CurrentPrev.Location, ErrorCode.Expected, "Llaves no balanceadas. "));
                    if(ifOpen > 0)
                        program.errors.AddError(new CompilingError(ifToken.Location, ErrorCode.Invalid, "If mal conformado. "));

                    //si el token q se busca no se encuentra, entonces devolver al tokenConsumer a la posicion original q tenia.
                    c.setPosition(pos);
                    return null;
                }
                tokens.Add(c.Current);

            } while (!(opens == 0 && seqOpens == 0 && letOpens == 0 && ifOpen == 0 && (c.Current.Value == tokenValue ||
                                                                      (secondaryTokenValue != null && c.Current.Value == secondaryTokenValue))));

            //siempre terminar la expression con ;
            tokens.RemoveAt(tokens.Count - 1);
            tokens.Add(new Token(TokenType.Symbol, TokenValues.StatementSeparator, new GeoWallE.GraphicRuntime.Core.CodeLocation()));

            removeExtraStatementSeparators(tokens);
            //el token secundario es para poder dejar el token consumer justo en la posicion de tal token, sirve para q funcall sepa cuando se encuentra el )
            if (secondaryTokenValue == null || c.Current.Value != secondaryTokenValue)
                c.Next();

            //termina o en el proximo token despues de una , o encima de ).
            return new TokenConsumer(tokens);
        }
        public static void removeExtraStatementSeparators(List<Token> tokens)
        {
            for (int i = tokens.Count -1 ; i > 1; i--)
            {
                if (tokens[i].Value == TokenValues.StatementSeparator && tokens[i - 1].Value == TokenValues.StatementSeparator)
                    tokens.RemoveAt(i);
                else return;
            }
        }
    }
}
