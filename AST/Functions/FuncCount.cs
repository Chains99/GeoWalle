using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncCount : DeclarationFun
    {
        string p1 = "p1";        
        public FuncCount()
        {
            funcName = "count";
            list = new List<string>() { p1 };
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var r = new GSharpNumber();
            var seq = scope.getVar(p1) as GSharpSeq;
            if (seq.isInfinite)
                r.Value = double.PositiveInfinity;
            else
                foreach (var item in seq.values)
                    r.Value++;
            return r;
        }

    }
}
