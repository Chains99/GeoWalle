using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncLine : DeclarationFun
    {
        string p1 = "p1";
        string p2 = "p2";
        public FuncLine()
        {
            funcName = "line";
            list = new List<string>() { p1, p2 };
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var r = new GSharpLine();
            r.P1 = scope.getVar(p1) as GSharpPoint;
            r.P2 = scope.getVar(p2) as GSharpPoint;
            return r;
        }

    }
}
