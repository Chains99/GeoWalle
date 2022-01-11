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
    class InfinitySequence:Sequence
    {
        Expression a;
        int init;
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
            
            if (context.Current.Value != TokenValues.ClosedCurlyBraces)
                return false;
            if (!context.Next())
                return false;

            return true;
        }
        public override GSharpObject eval(Scope scope)
        {
            init = (int)(a.eval(scope) as GSharpNumber).Value;
            var v = new GSharpSeq();
            v.values = enumerator();
            v.isInfinite = true;
            return v;       
        }

        IEnumerable<GSharpNumber> enumerator()
        {
            var enumerableInit = init;
            while (true)
            {
                var v = new GSharpNumber();
                v.Value = enumerableInit++;
                yield return v;
            }
        }
    }
}
