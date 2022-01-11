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
    class Equals : BinaryOp
    {       
        static string[] samePriorOp = { TokenValues.Less, TokenValues.LessOrEquals, TokenValues.GreaterOrEquals, TokenValues.Greater, TokenValues.NotEquals };
        

        public override GSharpObject eval(Scope scope)
        {
            var l = Left.eval(scope);
            var r = Rigth.eval(scope);
            return (l as IEquals ).Equalsresult(r);    
        }
       
        protected override string getOper()
        {
            return TokenValues.Equals;
        }
        
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }

    }
}
