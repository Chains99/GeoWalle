using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace AST
{
    public class FuncIntersect : DeclarationFun
    {
        string p1 = "p1";
        string p2 = "p2";
        public FuncIntersect()
        {
            funcName = "intersect";
            list = new List<string>() { p1, p2 };
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {            
            var P1 = scope.getVar(p1) as GSharpObject as Iintersect;
            var P2 = scope.getVar(p2) as GSharpObject;
            
            return P1.intersect(P2);
        }

    }
}
