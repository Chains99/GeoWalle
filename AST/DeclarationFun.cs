using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime;

namespace AST
{
    public class DeclarationFun:Expression
    {

        public List<string> list;
        public string funcName;
        public Expression der;
        public override bool build(TokenConsumer context)
        {
            list = new List<string>();

            if (context.Current.Type != TokenType.Identifier)
                return false;
            this.funcName = context.Current.Value;
            if (!context.Next())
                return false;
            if (context.Current.Value != TokenValues.OpenBracket)
                return false;
            if (!context.Next())
                return false;

            if (context.Current.Type == TokenType.Identifier)
            {
                list.Add(context.Current.Value);
                if (!context.Next())
                    return false;
            }
                
            while (context.Current.Value != TokenValues.ClosedBracket)
            {                
                if (context.Current.Value != TokenValues.ValueSeparator)
                    return false;
                if (!context.Next())
                    return false;
                if (context.Current.Type != TokenType.Identifier)
                    return false;
                list.Add(context.Current.Value);

                if (!context.Next())
                    return false;
            }
            if (!context.Next())
                return false;
            if (context.Current.Value != TokenValues.Assign)
                return false;
            if (!context.Next())
                return false;
            der = new Expression();
            if (!der.build(context))
                return false;
            return true;                 
        }

        public override GSharpObject eval(Scope scope)
        {
            scope.setFunc(funcName, this);
            return null;
        }
       
    }
}
