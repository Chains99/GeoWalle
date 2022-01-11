using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncCircle:DeclarationFun
    {
        string radio = "r";
        string center = "p";
        public FuncCircle()
        {
            funcName = "circle";
            list = new List<string>() {center, radio};
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var c = new GSharpCircle();
            c.Center = scope.getVar(center) as GSharpPoint;
            var r= scope.getVar(radio);

            if (r is GSharpMeasure)
                c.Radius = (int)(r as GSharpMeasure).Value;
            else if (r is GSharpNumber)
                c.Radius = (int)(r as GSharpNumber).Value;
            return c;
        }


    }
}
