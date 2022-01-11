using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime.Types
{
    public class NumberType : GSharpType
    {
        public GSharpType Result(GSharpType rightType)
        {
            if (rightType is NumberType)
                return this;
            // TODO: si el tipo derecho es 'dynamic', devolver el mismo 'dynamic'

            return null;
        }
    }
}
