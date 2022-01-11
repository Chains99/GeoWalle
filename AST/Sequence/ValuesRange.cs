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
    class ValuesRange:Sequence
    {
        Expression a;
        Expression b;

        int start = 0;
        int finish = 0;

        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.OpenCurlyBraces)
                return false;
            if (!context.Next())
                return false;

            var consumer = program.getNextExpTokens(context, TokenValues.Dots, null);
            if (consumer == null)
                return false;
            a = new Expression();

            if (!a.build(consumer))
                return false;

            consumer = program.getNextExpTokens(context, TokenValues.ClosedCurlyBraces, null);
            if (consumer == null)
                return false;

            b = new Expression();
            if (!b.build(consumer))
                return false;            
            
            return true;
        }

        public override GSharpObject eval(Scope scope)
        {
            start = (int)(a.eval(scope) as GSharpNumber).Value;
            finish = (int)(b.eval(scope) as GSharpNumber).Value;

            var seq = new GSharpSeq();
            seq.values = values();
            return seq;
        }

        IEnumerable<GSharpObject> values()
        {

            for (int i = start; i < finish; i++)
            {
                var n = new GSharpNumber();
                n.Value = i;
                yield return n; 
            }

        }
    }
}
