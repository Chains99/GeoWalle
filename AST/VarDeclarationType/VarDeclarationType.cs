using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    public class VarDeclarationType:Expression
    {
        Expression expr;
        public override bool build(TokenConsumer context)
        {
            expr = new PointType();
            if (Verify(context))
                return true;
            expr = new LineType();
            if (Verify(context))
                return true;
            expr = new CircleType();
            if (Verify(context))
                return true;
            expr = new SegmentType();
            if (Verify(context))
                return true;
            expr = new RayType();
            if (Verify(context))
                return true;
            expr = new ArcType();
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
        public override GSharpObject eval(Scope scope)
        {
            return expr.eval(scope);
        }
    }
}
