using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Core;

namespace AST
{
    class IFExpression:Expression
    {
        Expression expif;
        Expression expthen;
        Expression expelse; 
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.If)
                return false;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.CurrentPrev.Location, ErrorCode.Expected, "Expression if no completada "));
                return false; 
            }

            var consumer = program.getNextExpTokens(context, TokenValues.Then, null);
            expif = new Expression();
            if (!expif.build(consumer))
                return false;

            consumer = program.getNextExpTokens(context, TokenValues.Else, null);
            expthen = new Expression();
            if (!expthen.build(consumer))
                return false;

            expelse = new Expression();
            if (!expelse.build(context))
                return false;                                           

            return true;
              
        }
        public override GSharpObject eval(Scope scope)
        {
            var n = expif.eval(scope);

            if (n.TrueOrFalse().Value == 0)
                return expelse.eval(scope);
            return expthen.eval(scope);

        }

    }
}
