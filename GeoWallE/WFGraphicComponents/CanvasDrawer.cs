using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;
using System.Drawing;
using System;

namespace GeoWallE.WFGraphicComponents
{
    public class CanvasDrawer : GDrawer, IDrawer<GSharpPoint>, IDrawer<GSharpCircle>, IDrawer<GSharpLine>,IDrawer<GSharpRay>, IDrawer<GSharpSegment>, IDrawer<GSharpArc>
    {
        private readonly Canvas _canvas;

        public CanvasDrawer(Canvas canvas)
        {
            _canvas = canvas;
        }       

        public void Draw(GSharpPoint obj, string label)
        {
            var point = new PointF((float)obj.X, (float)obj.Y);

            _canvas.DrawPoint(point, label);
            _canvas.Invalidate();
        }

        public void Draw(GSharpCircle obj, string label)
        {
            var center = new PointF((float)obj.Center.X, (float)obj.Center.Y);
            double radius = obj.Radius;

            _canvas.DrawCircle(center, radius, label);
            _canvas.Invalidate();
        }

        public void Draw(GSharpLine obj, string label)
        {
            var p1 = new PointF((float)obj.P1.X,(float) obj.P1.Y);
            var p2 = new PointF((float)obj.P2.X, (float)obj.P2.Y);

            _canvas.DrawLine(p1, p2, label);
            _canvas.Invalidate();
        }
        public void Draw(GSharpSegment obj,string label)
        {
            var p1 = new PointF((float)obj.P1.X, (float)obj.P1.Y);
            var p2 = new PointF((float)obj.P2.X, (float)obj.P2.Y);

            _canvas.DrawSegment(p1, p2, label);
            _canvas.Invalidate();
        }

        public void Draw(GSharpRay obj, string label)
        {
            var p1 = new PointF((float)obj.P1.X, (float)obj.P1.Y);
            var p2 = new PointF((float)obj.P2.X, (float)obj.P2.Y);

            _canvas.DrawRay(p1, p2, label);
            _canvas.Invalidate();
        }
        public override void SetColor(Color color)
        {
            this._canvas.SetColor(color);
        }
        public override Color getColor()
        {
            return this._canvas.GetColor();

        }

        public new void Draw(GSharpArc obj, string label)
        {
            Point center = obj.Center.asPoint();
            Point start = obj.P1.asPoint();
            Point end = obj.P2.asPoint();

            this._canvas.DrawArc(center, start, end, (int)obj.Radius, label);   
        }
    }
}
