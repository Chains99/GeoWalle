using GeoWallE.GraphicRuntime.Types.Interfaces;
using System;

namespace GeoWallE.GraphicRuntime.Types
{
    public class SequenceType : GSharpType
    {
        public GSharpType InnerType { get; set; }
       
    }
}
