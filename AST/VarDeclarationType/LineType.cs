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
    class LineType:VarDeclarationType
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Line)
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
            var line = new GSharpLine();
            line.P1 = PointType.GetRandomPoint();
            line.P2 = PointType.GetRandomPoint();
            scope.setVar(id, line);

            return null;

            
        }
    }
}
