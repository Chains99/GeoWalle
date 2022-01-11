using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Types.Interfaces_booleanas;

namespace AST
{
    class GreaterOrEquals : BinaryOp
    {
        
        static string[] samePriorOp = { TokenValues.Less, TokenValues.LessOrEquals, TokenValues.Greater, TokenValues.Equals, TokenValues.NotEquals };
        

        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IGreaterOrEquals).GorE(r);

        }

        protected override string getOper()
        {
            return TokenValues.GreaterOrEquals;
        }
       
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}
