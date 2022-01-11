using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncPoint : DeclarationFun
    {
        string p1 = "x";
        string p2 = "y";
        public FuncPoint()
        {
            funcName = "point";
            list = new List<string>() { p1, p2 };
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var p = new GSharpPoint();
            p.X = (int)(scope.getVar(p1) as GSharpNumber).Value;
            p.Y = (int)(scope.getVar(p2) as GSharpNumber).Value;
            return p;
        }

    }
}
