using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis; 
using GeoWallE.Parsing;
using GeoWallE.GraphicRuntime.Core;
namespace AST
{
    public class SeqExpresion : Node
    {
        List<Expression> exprs;
        
        List<Token> tokenlist;
        public override bool build(TokenConsumer context)
        {
            Expression expr;
            this.exprs = new List<Expression>();
            int open = 0;
            while (true)
            {
                if (context.EndOfTokens)
                    return true;

                var consumer = program.getNextExpTokens(context, TokenValues.StatementSeparator, null);
                if (consumer == null)
                    return false;
                expr = new Expression();
                if (!expr.build(consumer))
                    return false;
                exprs.Add(expr);          
            }
        }
                //while (true)
                //{
                //    int pos = context.getPosition();
                //    expr = new Expression();
                //    if (!expr.build(context)) {
                //        context.setPosition(pos);
                //        return false;
                //    }      

                //    if(context.Current.Value == TokenValues.StatementSeparator)
                //    {

                //        this.exprs.Add(expr);
                //        if (!context.Next())
                //            return true;
                //    }


                //    else
                //    {
                //        program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "Se esperaba ;."));
                //        return false;

                //    }  

            


    
        public override GSharpObject eval(Scope scope)
        {
            foreach(Expression expr in exprs)            
                expr.eval(scope);            
            return null;
        }
    }
}
