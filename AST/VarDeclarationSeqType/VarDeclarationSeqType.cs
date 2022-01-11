using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime;

namespace AST
{
    class VarDeclarationSeqType:Expression
    {
        Expression expr;
        public override bool build(TokenConsumer context)
        {
            expr = new PointSequence();
            if (Verify(context))
                return true;
            expr = new LineSequence();
            if (Verify(context))
                return true;
            expr = new CircleSequence();
            if (Verify(context))
                return true;
            expr = new SegmentSequence();
            if (Verify(context))
                return true;
            expr = new RaySequence();
            if (Verify(context))
                return true;
            return false;
        }
        private bool Verify(TokenConsumer context)
        {

            int pos = context.getPosition();
            if (!expr.build(context))
            {
                context.setPosition(pos);
                return false;
            }
            return true;
        }
    }
}
