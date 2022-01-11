using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;

namespace AST
{
    public class FuncRandoms : DeclarationFun
    {
        public FuncRandoms()
        {
            funcName = "randoms";
            list = new List<string>();
            der = this;
        }

        public override GSharpObject eval(Scope scope)
        {
            var r = new GSharpSeq();
            r.isInfinite = true;
            r.values = values();           
            return r;
        }

        IEnumerable<GSharpNumber> values()
        {
            while(true)
            {
                var n = new GSharpNumber();
                n.Value = PointType.random.NextDouble();
                yield return n;
            }
        }

    }
}
