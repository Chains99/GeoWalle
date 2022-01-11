using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime;

namespace AST
{
    class Or : BinaryOp
    {
       
        static string[] samePriorOp = { TokenValues.Or };
        
        public override GSharpObject eval(Scope scope)
        {
            var r = Rigth.eval(scope);
            var l = Left.eval(scope);
            if (r.TrueOrFalse().Value == 1 || l.TrueOrFalse().Value == 1)
            {

                var result = new GSharpNumber();
                result.Value = 1;
                return result;
            }
            else
            {
                var result = new GSharpNumber();
                result.Value = 0;
                return result;
            }


        }
       
        protected override string getOper()
        {
            return TokenValues.Or;
        }
        
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}
