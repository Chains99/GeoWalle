using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    public class Expression : Node
    {
        Expression expr;
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
       
        public override bool build(TokenConsumer context)
        {
           
            this.expr = new UnaryOp();
            if (Verify(context))
                return true;

            this.expr = new Commands();
            if (Verify(context))
                return true;
            this.expr = new Undefined();
            if (Verify(context))
                return true;            

            this.expr = new VarDeclarationSeqType();
            if (Verify(context))
                return true;

            this.expr = new DeclarationFun();
            if (Verify(context))
                return true;

            this.expr = new IFExpression();
            if (Verify(context))
                return true;

            this.expr = new LetinExpression();
            if (Verify(context))
                return true;

            this.expr = new Assignment();
            if (Verify(context))
                return true;
            
            this.expr = new BinaryOp();
            if (Verify(context))
                return true;

            this.expr = new FunCall();
            if (Verify(context))
                return true;

            this.expr = new VarDeclarationType();
            if (Verify(context))
                return true;

            this.expr = new VarDefined();
            if (Verify(context))
                return true;

            this.expr = new Number();
            if (Verify(context))
                return true;

            this.expr = new Sequence();
            if (Verify(context))
                return true;

            this.expr = new ParExpression();
            if (Verify(context))
                return true;

            return false;
        }
        public override GSharpObject eval(Scope scope)
        {
            return expr.eval(scope);
        }
    }
}

