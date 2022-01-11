using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class BinaryOp:Expression
    {
        protected Expression Rigth;
        protected Node Left;
        Expression exp;


        public override bool build(TokenConsumer context)
        {

            exp = new And();
            if (Verify(context))
                return true;

            exp = new Or();
            if (Verify(context))
                return true;

            exp = new Equals();
            if (Verify(context))
                return true;

            exp = new Less();
            if (Verify(context))
                return true;

            exp = new LessOrEquals();
            if (Verify(context))
                return true;

            exp = new Greater();
            if (Verify(context))
                return true;

            exp = new GreaterOrEquals();
            if (Verify(context))
                return true;

            exp = new NotEquals();
            if (Verify(context))
                return true;

            exp = new Adicion();
            if (Verify(context))
                return true;

            exp = new Substraccion();
            if (Verify(context))
                return true;

            exp = new Multiplicacion();
            if (Verify(context))
                return true;

            exp = new Division();
            if (Verify(context))
                return true;


            exp = new Mod();
            if (Verify(context))
                return true;

            return false;


        }
        public override GSharpObject eval(Scope scope)
        {
            return exp.eval(scope);
        }
        private bool Verify(TokenConsumer context)
        {
            int pos = context.getPosition();
            if (!(exp as BinaryOp).buildBinary(context))
            {
                context.setPosition(pos);
                return false;
            }
            return true;
        }

        protected bool buildBinary(TokenConsumer context)
        {            

            int pos;
            int pos1;            
            int op = -1;
            int op_same_prior = -1;

            int opensPar = 0;
            int opensLet = 0;
            int openCurly = 0;

            var tokenlist = new List<Token>();
            if (context.Current.Value == TokenValues.Add)
                return false;
            pos = context.getPosition();

            while (context.getPosition() != context.Count() - 1)
            {
                if (context.Current.Value == TokenValues.OpenBracket)
                    opensPar++;
                else if (context.Current.Value == TokenValues.ClosedBracket)
                    opensPar--;
                else if (context.Current.Value == TokenValues.Let)
                    opensLet++;
                else if (context.Current.Value == TokenValues.In)
                    opensLet--;
                else if (context.Current.Value == TokenValues.OpenCurlyBraces)
                    openCurly++;
                else if (context.Current.Value == TokenValues.ClosedCurlyBraces)
                    openCurly--;

                if (opensLet == 0 && opensPar == 0 && openCurly ==0)
                {
                    if (context.Current.Value == getOper())
                        op = context.getPosition();

                    else if (Array.IndexOf(getSamePriorOperators(), context.Current.Value) != -1)
                        op_same_prior = context.getPosition();
                }

                if (!context.Next())
                    return false;
            }

            if (op == -1)
                return false;
            if (op_same_prior > op)
                return false;
            pos1 = op + 1;
            context.setPosition(pos);
            for (int i = pos; i < op; i++)
            {
                tokenlist.Add(context.Current);
                context.Next();
            }
            context.setPosition(pos1);

            Rigth = new Expression();
            if (!Rigth.build(context))
                return false;

            Left = new Expression();
            tokenlist.Add(context.Current);
            if (!Left.build(new TokenConsumer(tokenlist)))
                return false;

            return true;
        }
        protected virtual string getOper()
        {
            throw new NotImplementedException();
            //return "";
        }
       
        protected virtual string[] getSamePriorOperators()
        {
            throw new NotImplementedException();
            //return new String[] { };
        }
    }
}
                                