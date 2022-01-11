using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Core;

namespace AST
{
    public abstract class Node
    {
        
        public abstract bool build(TokenConsumer context);

        //public abstract bool checksemantic(Scope scope);

        //public abstract GSharpObject returnType();

        public abstract GSharpObject eval(Scope scope);
        
        
    }
}
