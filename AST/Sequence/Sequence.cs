using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class Sequence:Expression
    {
        Expression exp;
        public override bool build(TokenConsumer context)
        {           
            exp = new ValuesRange();
            if (Verify(context))
                return true;
            exp = new InfinitySequence();
            if (Verify(context))
                return true;
            exp = new FinitySequence();
            if (Verify(context))
                return true;
            return false;

        }
        private bool Verify(TokenConsumer context)
        {
            int pos = context.getPosition();
            if (!exp.build(context))
            {
                context.setPosition(pos);
                return false;
            }
            return true;
        }
        public override GSharpObject eval(Scope scope)
        {
            return exp.eval(scope);
        }
    }
}
