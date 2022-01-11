using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    class FinitySequence : Sequence
    {
        List<Expression> exp;
        public override bool build(TokenConsumer context)
        {
            exp = new List<Expression>();
            if (context.Current.Value != TokenValues.OpenCurlyBraces)
                return false;
            if (!context.Next())
                return false;
            while (context.Current.Value != TokenValues.ClosedCurlyBraces)
            {
                var consumer2 = program.getNextExpTokens(context, TokenValues.ValueSeparator, TokenValues.ClosedCurlyBraces);
                if (consumer2 == null)
                    return false;

                Expression expression = new Expression();
                if (!expression.build(consumer2))
                    return false;
                exp.Add(expression);
            }
            if (!context.Next())
                return false;
            return true;
        }

        public override GSharpObject eval(Scope scope)
        {
            var seq = new GSharpSeq();
            seq.values = values(scope);
            return seq;
        }

        public IEnumerable<GSharpObject> values(Scope scope)
        {
            foreach (var item in exp)
            {
                yield return item.eval(scope);
            }

        }
    }
}
