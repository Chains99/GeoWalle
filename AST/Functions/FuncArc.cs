using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncArc : DeclarationFun
    {
        string p1 = "p1";
        string p2 = "p2";
        string p3 = "p3";
        string m = "m";
        public FuncArc()
        {
            funcName = "arc";
            list = new List<string>() { p1, p2,p3,m};
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var r = new GSharpArc();
            r.Center = scope.getVar(p1) as GSharpPoint;
            r.P1 = scope.getVar(p2) as GSharpPoint;
            r.P2 = scope.getVar(p3) as GSharpPoint;
            r.Radius = (scope.getVar(m) as GSharpMeasure).Value;
            
            return r;
        }

    }
}
