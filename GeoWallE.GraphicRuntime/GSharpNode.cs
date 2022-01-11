using GeoWallE.GraphicRuntime.Core;

namespace GeoWallE.GraphicRuntime
{
    public abstract class GSharpNode
    {
        public abstract bool IsOk { get; }

        public abstract void CheckSemantics(GSharpContext context, OutputInfo outputInfo);
    }
}
