using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncMeasure : DeclarationFun
    {
        string p1 = "p1";
        string p2 = "p2";
        public FuncMeasure()
        {
            funcName = "measure";
            list = new List<string>() { p1, p2 };
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {            
            var p_1 = scope.getVar(p1) as GSharpPoint;
            var p_2 = scope.getVar(p2) as GSharpPoint;
            return new GSharpMeasure(p_1, p_2);
        }

    }
}
