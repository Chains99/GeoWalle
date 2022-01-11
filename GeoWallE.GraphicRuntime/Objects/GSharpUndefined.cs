using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpUndefined : GSharpObject, ISummable
    {
        public override GSharpType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return "undefined";
        }
        public GSharpObject SumResult(GSharpObject rightType)
        {
           return this; 
        }
    }
}
