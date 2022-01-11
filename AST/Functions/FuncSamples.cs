using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncSamples : DeclarationFun
    {
        public FuncSamples()
        {
            funcName = "samples";
            list = new List<string>() ;
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var r = new GSharpSeq();
            r.isInfinite = true;
            r.values = values();
            return r;
        }
        IEnumerable<GSharpPoint> values()
        {
            while (true)
                yield return PointType.GetRandomPoint();
        }

    }
}
