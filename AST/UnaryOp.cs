using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Core;

namespace AST
{
    class UnaryOp : Expression
    {
        Expression exp;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Not)
                return false;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "se esperaba una Expresion"));
                return false;
            }
            exp = new Expression();
            if (!exp.build(context))
                return false;
            
            return true;

        }
        public override GSharpObject eval(Scope scope)
        {
            var n = exp.eval(scope);
            if (n.TrueOrFalse().Value == 0)
            {
                var result = new GSharpNumber();
                result.Value = 1;
                return result;
            }
            else
            {
                var result = new GSharpNumber();
                result.Value = 0;
                return result;
            }                          
        }
    }
}
