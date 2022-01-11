using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime
{
    public abstract class GSharpType
    {
        public virtual bool IsDrawable
        {
            get { return this is IDrawable; }
        }
    }
}
