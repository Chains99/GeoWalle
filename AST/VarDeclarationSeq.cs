using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    public class VarDeclarationSeq : Expression
    {
        
        public List<string> ids;
        public override bool build(TokenConsumer context)
        {
            ids = new List<string>();
            if (context.Current.Type != TokenType.Identifier && context.Current.Value != TokenValues._)
                return false;
            ids.Add(context.Current.Value);                
            if (!context.Next() )
            {
                return false;
            }
            if (context.Current.Value != TokenValues.ValueSeparator)
                return false;
            if (!context.Next()) 
                return false;
            if (context.Current.Type != TokenType.Identifier && context.Current.Value != TokenValues._)
                return false;
            ids.Add(context.Current.Value);
            if (!context.Next())
                return false;

            while (context.Current.Value!=TokenValues.Assign)
            { 
                
                if (context.Current.Value != TokenValues.ValueSeparator)
                    return false;
                if (!context.Next())
                    return false;
                if (context.Current.Type != TokenType.Identifier && context.Current.Value != TokenValues._)
                    return false;
                ids.Add(context.Current.Value);
                if (!context.Next())
                    return false;
            }
            
            return true;
       
        }
        public override GSharpObject eval(Scope scope)
        {
            return null;
        }
    }
}
