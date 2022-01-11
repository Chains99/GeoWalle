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
    class Greater : BinaryOp
    {        
        static string[] samePriorOp = { TokenValues.Less, TokenValues.LessOrEquals, TokenValues.GreaterOrEquals, TokenValues.Equals, TokenValues.NotEquals };
        

        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IGreater).GreaterResult(r);

        }
        
        protected override string getOper()
        {
            return TokenValues.Greater;
        }
        
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}
