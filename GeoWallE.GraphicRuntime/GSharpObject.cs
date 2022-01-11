using GeoWallE.GraphicRuntime.Objects;
using System;
using GeoWallE.GraphicRuntime.Types.Interfaces;
namespace GeoWallE.GraphicRuntime
    
{
    public abstract class GSharpObject
    {
        public abstract GSharpType Type { get; }
        public virtual GSharpNumber TrueOrFalse ()
        {
            throw new Exception();
        }
        public virtual bool IsDrawable
        {
            get { return this is IDrawable; }
        }
    }
}
