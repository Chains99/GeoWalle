﻿using System;
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
    class ArcType : VarDeclarationType
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Arc)
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
            var arc = new GSharpArc();
            arc.P1 = PointType.GetRandomPoint();
            arc.P2 = PointType.GetRandomPoint();
            arc.Center = PointType.GetRandomPoint();
            arc.Radius = PointType.random.Next(1, program.padding);
            scope.setVar(id, arc);
            return null;

        }
    }
}
