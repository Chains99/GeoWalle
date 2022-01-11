using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
namespace AST
{
    public class FunCall : Expression
    {
        string idFunction;
        List<Expression> exp;
        
        bool isFigureConstructor(Token token)
        {
            return (token.Value == TokenValues.Circle ||
                    token.Value == TokenValues.Ray ||
                    token.Value == TokenValues.Segment ||
                    token.Value == TokenValues.Line ||
                    token.Value == TokenValues.Point ||
                    token.Value == TokenValues.Arc);
        }
        public override bool build(TokenConsumer context)
        {

            exp = new List<Expression>();

            if (context.Current.Type != TokenType.Identifier && !isFigureConstructor(context.Current))
                //add error
                return false; 
            idFunction = context.Current.Value;
            //como el lexer reconoce a cicle y le pone valor Circle, tengo q llevarlo a minusculas para poder llamar a circle.
            if (isFigureConstructor(context.Current))
                idFunction = idFunction.ToLower();

            if (!context.Next())
                //add error
                return false;
            if (context.Current.Value != TokenValues.OpenBracket)
                //add error
                return false;
            if (!context.Next())
                return false;
            if(context.Current.Value == TokenValues.ClosedBracket)
            {
                if (!context.Next())
                    return false;
                return true;
            }

            if (!buildExp(context))
                return false;
            while (context.Current.Value != TokenValues.ClosedBracket)
                if (!buildExp(context))
                    return false;

            if (!context.Next())
                return false;
                                                              
            return true;            
        }
        public override GSharpObject eval(Scope scope)
        {
            GSharpObject[] values = new GSharpObject[exp.Count];
            for (int i = 0; i < exp.Count; i++)
            {
                values[i] = exp[i].eval(scope);
            }
            var sc = new Scope(scope);
            var fun = scope.getFunc(idFunction);
            for (int i = 0; i < fun.list.Count; i++)
            {
                sc.setVar(fun.list[i], values[i]);
            }
            return fun.der.eval(sc);
        }

        
        bool buildExp(TokenConsumer context) {

            var consumer2 = program.getNextExpTokens(context, TokenValues.ValueSeparator, TokenValues.ClosedBracket);
            if (consumer2 == null)
                return false;

            Expression expression = new Expression();
            if (!expression.build(consumer2))
                return false;
            exp.Add(expression);
            return true;
        }
    }
}
                  