using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    class Number:Expression
    {
        int  num;
        bool negative = false;
        public override bool build(TokenConsumer context)
        {
            //Esto puede que este o no
            if (context.Current.Value == TokenValues.Sub)
            {
                negative = true;
                if (!context.Next())
                    return false;
            }
            if (context.Current.Type != TokenType.Number)
                return false;
            num = int.Parse(context.Current.Value);
            if (negative)
                num = num * -1;
            if (!context.Next())
                return false;
            //El lexer no reconoce el caracter . por tanto numero no reconoce numeros con coma
            return true;
            
        }
        public override GSharpObject eval(Scope scope)
        {
            var result = new GSharpNumber();
            result.Value = num;
            return result;
        }


    }
}
