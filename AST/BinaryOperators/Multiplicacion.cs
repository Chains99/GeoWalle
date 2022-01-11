using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace AST
{
    class Multiplicacion : BinaryOp
    {
       
        static string[] samePriorOp = { TokenValues.Div, TokenValues.Mod };
      
    
        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IMultiplicationable).MultResult(r);
        }

        protected override string getOper()
        {
            return TokenValues.Mul;
        }
     
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}



