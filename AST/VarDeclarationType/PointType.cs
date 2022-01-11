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
    class PointType:VarDeclarationType
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Point)
                return false;

            if (!context.Next())
                return false;
            if (context.Current.Type != TokenType.Identifier)
            {
                program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid,"Aqui va un identificador."));
                return false;
            }
            id = context.Current.Value;
            if (!context.Next())
                return false;
            
            return true;
        }
        public override GSharpObject eval(Scope scope)
        {            
            scope.setVar(this.id, GetRandomPoint());
            return null;
        }
        public static Random random = new Random();
        public static GSharpPoint GetRandomPoint()
        {
            var p = new GSharpPoint();            
            int padd = program.padding;
            p.X = random.Next(0 + padd, program.maxX - padd);
            p.Y = random.Next(0 + padd, program.maxY - padd);
            return p;
        }
    }
}
