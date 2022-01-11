using System;
using System.Drawing;

namespace GeoWallE.GraphicRuntime
{
    public abstract class GDrawer
    {
        public void Draw<T>(T obj, string label = "")
        {
            if (this is IDrawer<T>)
            {
                (this as IDrawer<T>).Draw(obj, label);
            }
        }
        public abstract void SetColor(Color color);
        public abstract Color getColor();
    }

    public interface IDrawer<T>
    {
        void Draw(T obj, string label);
    }

    
}
