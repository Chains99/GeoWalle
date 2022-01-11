using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace AST
{
    class Substraccion : BinaryOp
    {        
        static string[] samePriorOp = { TokenValues.Add };
        
        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as ISubstractionable ).SubResult(r);
        }
        
        protected override string getOper()
        {
            return TokenValues.Sub;
        }
      
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}


        
