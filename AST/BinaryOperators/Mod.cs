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
    class Mod : BinaryOp
    {      
        static string[] samePriorOp = { TokenValues.Div, TokenValues.Mul };
       

        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IMod).ModResult(r);
        }

        protected override string getOper()
        {
            return TokenValues.Mod;
        }
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}

