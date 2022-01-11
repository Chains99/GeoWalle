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
    class Adicion : BinaryOp
    {                      
       
        static string[] samePriorOp = { TokenValues.Sub };
       
        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as ISummable).SumResult(r);
        }

        
        protected override string getOper()
        {
            return TokenValues.Add;
        }
       
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }

    }
}


        
    
