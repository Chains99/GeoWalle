using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime.Core;
namespace AST
{
    public class Assignment : Expression
    {
        Expression expr;
        Expression left;

        public override bool build(TokenConsumer context)
        {
            expr = new VarDeclarationSeq();

            if (!Verify(context))
            {
                expr = new VarDeclaration();
                if (!Verify(context))
                    return false;
            }
            left = expr;


            if (context.Current.Value != TokenValues.Assign)
            {               
                return false;
            }

            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.CurrentPrev.Location, ErrorCode.Expected, "Asignacion no completada "));
                return false;
            }
            expr = new Expression();
            if (!expr.build(context))
                return false;
            return true;

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
            if (left is VarDeclarationSeq)
            {
                var i = 0;
                var vv = expr.eval(scope) ;
                var l = left as VarDeclarationSeq;
                if (vv is GSharpSeq)
                {
                    var v = vv as GSharpSeq;
                    foreach (var item in v.values)
                    {
                        if (i < l.ids.Count - 1)
                            scope.setVar(l.ids[i], item);
                        else
                        {
                            var seq = new GSharpSeq();
                            seq.isInfinite = v.isInfinite;
                            seq.values = v.values;
                            seq.setPos(i);
                            scope.setVar(l.ids[i], seq);
                            return null;
                        }
                        i++;
                    }
                }
                for(;i < l.ids.Count; i++)
                {
                    scope.setVar(l.ids[i], new GSharpUndefined());
                }

            }
            else if (left is VarDeclaration)
            {
                var v = expr.eval(scope);
                var l = left as VarDeclaration;
                scope.setVar(l.id, v);
            }
            return null;
        }
    }
}


        
      

