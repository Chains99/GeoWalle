using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class Division : BinaryOp
    {
     
        static string[] samePriorOp = { TokenValues.Mul, TokenValues.Mod };
       
        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IDivisible).DivResult(r);    
        }


        protected override string getOper()
        {
            return TokenValues.Div;
        }
       
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}

