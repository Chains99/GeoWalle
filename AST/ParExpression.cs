using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class ParExpression:Expression
    {
        Expression expr;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value == TokenValues.OpenBracket)
            {
                int ct_parentesis = 0;
                int pos = -1;
                int begin = context.getPosition() + 1;
                List<Token> tokenlist = new List<Token>();

                if (!context.Next())
                    return false;

                while (!(context.Current.Value == TokenValues.ClosedBracket && ct_parentesis == 0))
                {
                    if (context.Current.Value == TokenValues.OpenBracket)
                        ct_parentesis++;
                    else if (context.Current.Value == TokenValues.ClosedBracket)
                    {
                        ct_parentesis--;
                        pos = context.getPosition();
                    }
                    if (!context.Next())
                        return false;
                }
                pos = context.getPosition();
                context.setPosition(begin);

                for (int i = begin; i < pos; i++)
                {
                    tokenlist.Add(context.Current);
                    context.Next();
                }
                tokenlist.Add(new Token(TokenType.Symbol, ";", new GeoWallE.GraphicRuntime.Core.CodeLocation()));

                expr = new Expression();
                if (Verify(new TokenConsumer(tokenlist)))
                    return true;
                return false;
            }
            return false;

        }

        public override GSharpObject eval(Scope scope)
        {
            return expr.eval(scope);
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

    }
}
