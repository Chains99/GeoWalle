using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    class And : BinaryOp  {
               
        static string[] samePriorOp = { TokenValues.Or };
       

        public override GSharpObject eval(Scope scope)
        {
            var r = Rigth.eval(scope);
            var l = Left.eval(scope);
            if (r.TrueOrFalse().Value == 0 || l.TrueOrFalse().Value == 0)
            {

                var result = new GSharpNumber();
                result.Value = 0;
                return result;
            }
            else
            {
                var result = new GSharpNumber();
                result.Value = 1;
                return result;
            }


        }

        protected override string getOper()
        {
            return TokenValues.And;
        }      
        protected override string[] getSamePriorOperators()
        {
            return samePriorOp;
        }
    }
}
