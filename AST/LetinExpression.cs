using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class LetinExpression:Expression
    {
        SeqExpresion expr;
        Expression expin;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Let)
                return false;
            if (!context.Next())
                return false;

            var consumer = program.getNextExpTokens(context, TokenValues.In, null);
            if (consumer == null)
                return false;
            expr = new SeqExpresion();
            if (!expr.build(consumer))
                return false;

            expin = new Expression();
            if (!expin.build(context))
                return false;
            
            return true;             
        }
        public override GSharpObject eval(Scope scope)
        {
            var sc = new Scope(scope);
            expr.eval(sc);
            return expin.eval(sc);
        }
    }
}
