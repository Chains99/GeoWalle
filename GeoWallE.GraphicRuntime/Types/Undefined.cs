using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoWallE.GraphicRuntime.Types
{
    class Undefined : GSharpObject
    {
        public override GSharpType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
