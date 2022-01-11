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
    class Undefined:Expression
    {
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Undefined)
                return false;
            if (!context.Next())
                return false;
            return true;
        }

        public override GSharpObject eval(Scope scope)
        {
            return new GSharpUndefined();
        }
    }
}
