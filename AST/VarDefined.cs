using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class VarDefined:Expression
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Type != TokenType.Identifier)
                return false;
            id = context.Current.Value;
            if (!context.Next())
                return false;
            return true;

        }
        public override GSharpObject eval(Scope scope)
        {
            var p = scope.getVar(id);
            if (p == null)
                throw new Exception("Variable no definida en el scope");
            return p; 
        }
    }
}
