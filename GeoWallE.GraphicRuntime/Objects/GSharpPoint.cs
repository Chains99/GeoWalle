using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Drawing;
using GeoWallE.GraphicRuntime.Types;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpPoint : GSharpObject, IDrawable,Iintersect
    {
        private static PointType _Type = new PointType();

        public double X { get; set; }

        public double Y { get; set; }

        public override GSharpType Type
        {
            get { return _Type; }
        }

        public override string ToString()
        {
            return $"[Point - ({X}, {Y})]";
        }

        GSharpSeq pointAsSeq()
        {
            GSharpSeq seq = new GSharpSeq();
            seq.values = me();
            return seq;
        }
        public GSharpObject intersect(GSharpObject obj)
        {
            if (obj is GSharpPoint)
            {
                GSharpPoint point = (GSharpPoint)obj;
                if (X == point.X && Y == point.Y)
                    return new GSharpUndefined();
                else
                {
                    return pointAsSeq();
                }
            }
            if (obj is GSharpLine)
                return intersectsWithLine(this, obj as GSharpLine);
            if (obj is GSharpCircle)
                return intersectsWithCircle(this, obj as GSharpCircle);
            if (obj is GSharpSegment)
                return intersectsWithSegment(this, obj as GSharpSegment);
            if (obj is GSharpRay)
                return intersectsWithRay(this, obj as GSharpRay);
            if (obj is GSharpArc)
                return GSharpArc.intersectWithAll(obj as GSharpArc, this);

            return null;
        }
        public Point asPoint()
        {
            return new Point((int)X, (int)Y);
        }

        private IEnumerable<GSharpObject> me()
        {
            yield return this;
        }

        public void Draw( GDrawer drawer, string label="")
        {
            drawer.Draw(this, label);
        }
        public GSharpPoint Normalize()
        {
            var p = new GSharpPoint();
            var dist = GSharpPoint.distancePointPoint(new GSharpPoint(), this);
            p.X = p.X / dist;
            p.Y = p.Y / dist;
            return p;
        }

        public static GSharpObject intersectsWithLine(GSharpPoint p, GSharpLine line)
        {
            var m = line.P1.Y - line.P2.Y / line.P1.X - line.P2.X;
            var result = m * (p.X - line.P1.X) + line.P1.Y - p.Y;
            if (result == 0)
                return p.pointAsSeq();

            return new GSharpUndefined();
        }

        public static GSharpObject intersectsWithCircle(GSharpPoint p, GSharpCircle circle)
        {
            var result = Math.Pow((p.X - circle.Center.X), 2) - Math.Pow((p.Y - circle.Center.Y), 2) - Math.Pow(circle.Radius, 2);
            if (result == 0)
                return p.pointAsSeq();
            return new GSharpUndefined();
        }

        public static GSharpObject intersectsWithSegment(GSharpPoint p, GSharpSegment segment)
        {
            var l = segment.GetLine();
            if (!(intersectsWithLine(p, l) is GSharpUndefined))
            {
                if (distancePointPoint(p, segment.P1) == distancePointPoint(p, segment.P2))
                    return p.pointAsSeq();               
            }
            return new GSharpUndefined();
        }

        public static GSharpObject intersectsWithRay(GSharpPoint p, GSharpRay ray)
        {
            GSharpPoint norm_ray = ray.GetLine().getNormalizedVector();
            GSharpLine l = new GSharpLine(ray.P1,p);
            GSharpPoint norm_p = l.getNormalizedVector();

            if(norm_p.X == norm_ray.X && norm_p.Y == norm_ray.Y)
            {
                return p.pointAsSeq();
            }
            return new GSharpUndefined();
        }

        public static float distancePointPoint(GSharpPoint p1, GSharpPoint p2)
        {
            return (float)Math.Round(Math.Sqrt(Math.Pow(p1.X - p2.X, 2) - Math.Pow(p1.Y -p2.Y, 2)),4);
        }
    }
}
