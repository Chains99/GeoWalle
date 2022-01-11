using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using GeoWallE.GraphicRuntime.Core;

namespace AST
{
    class Draw : Commands
    {
        Expression expr;
        string label;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Draw)
                return false;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "Draw esperaba una expresion."));
                return false;
            }
            //Esto es opcional por tanto.
            var consumer = program.getNextExpTokens(context, TokenValues.ValueSeparator, null);
            expr = new Expression();
            if (consumer != null)
            {
                if (!expr.build(consumer))
                    return false;
                if (context.Current.Type != TokenType.Text)
                {
                    program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "Draw esperaba un texto."));
                    return false;
                }
                label = context.Current.Value;
            }
            else
            {
                if (expr.build(context))
                    return true;
            }


            return true;

        }
        public override GSharpObject eval(Scope scope)
        {
            var obj = expr.eval(scope);
            if(obj is GSharpSeq)
            {
                if((obj as GSharpSeq).isInfinite)
                {
                    program.output.Text += "No se puede dibujar una secuencia inifita";
                    return null;
                }
            }
            if (obj is GSharpUndefined)
                return null;
            var drawable = obj as IDrawable;
            drawable.Draw(program.drawer, label);
            return null;


        }

    }
}
