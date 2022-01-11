using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Core;

namespace AST
{
    public class SegmentType : VarDeclarationType
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Segment)
                return false;
            if (!context.Next())
                return false;
            if (context.Current.Type != TokenType.Identifier)
            {
                program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "Aqui va un identificador."));
                return false;
            }
            id = context.Current.Value;
            if (!context.Next())
                return false;

            return true;
        }
        public override GSharpObject eval(Scope scope)
        {
            var segment = new GSharpSegment();
            segment.P1 = PointType.GetRandomPoint();
            segment.P2 = PointType.GetRandomPoint();
            scope.setVar(id, segment);
            return null;
        }

    }
}
