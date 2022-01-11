using System;
using GeoWallE.GraphicRuntime.Types;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using System.Collections.Generic;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpLine : GSharpObject, IDrawable, Iintersect
    {
        private static LineType _Type = new LineType();

        public GSharpPoint P1 { get; set; }

        public GSharpPoint P2 { get; set; }
        public GSharpLine()
        {

        }
        public GSharpLine(GSharpPoint p1, GSharpPoint p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public override GSharpType Type
        {
            get { return _Type; }
        }

        public void Draw(GDrawer drawer, string label)
        {
            drawer.Draw(this, label);
        }
    
        GSharpSeq pointAsSeq(params GSharpPoint[] points)
        {
            GSharpSeq seq = new GSharpSeq();
            seq.values = me(points);
            return seq;
        }
        private IEnumerable<GSharpObject> me(params GSharpPoint[] points)
        {
            foreach (var item in points)
            {
                yield return item;
            }

        }

        public GSharpObject intersect(GSharpObject obj)
        {
            if (obj is GSharpPoint)
                return GSharpPoint.intersectsWithLine(obj as GSharpPoint, this);

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
            return new GSharpUndefined();
        }

      
        public override string ToString()
        {
            return $"[Line - P1: {P1}, P2: {P2}]";
        }

        public GSharpPoint getNormalizedVector()
        {
            var g = new GSharpPoint();
            var dist = GSharpPoint.distancePointPoint(P2, P1);
            g.X = (P2.X - P1.X) / dist;
            g.Y = (P2.Y - P1.Y) / dist;
            return g;
        }
        public static GSharpObject intersectsWithLine(GSharpLine line1, GSharpLine line2)
        {
            var m1 = CalculatePend(line1.P1, line1.P2);
            var m2 = CalculatePend(line2.P1, line2.P2);
            GSharpPoint point = new GSharpPoint();
            if (m2 == m1)
                return new GSharpUndefined();
            point.X = (line2.P2.Y - line1.P2.Y + (m1 * line1.P2.X) - (m2 * line2.P2.X)) / (m1 - m2);
            point.Y = -1 * (m1 * (line1.P1.X - point.X) - line1.P1.Y);


            return line1.pointAsSeq(point);
        }
        static double CalculatePend(GSharpPoint p1, GSharpPoint p2)
        {
            return (p1.Y - p2.Y) / (p1.X - p2.X);
        }
        public static GSharpObject intersectsWithCircle(GSharpLine line1, GSharpCircle circle)
        {
            var m = CalculatePend(line1.P1, line1.P2);
            if (m == double.MaxValue)
               

            {
                if (line1.P1.X < circle.Center.X + circle.Radius && line1.P1.X > circle.Center.X - circle.Radius)
                {
                    double side = Math.Sqrt(Math.Pow(circle.Radius, 2) - Math.Pow(Math.Abs(circle.Center.X - line1.P1.X), 2));
                    GSharpPoint p1 = new GSharpPoint();
                    GSharpPoint p2 = new GSharpPoint();
                    p1.X = line1.P1.X;
                    p1.Y = circle.Center.Y + side;
                    p2.X = line1.P1.X;
                    p2.Y = circle.Center.Y - side;

                   return line1.pointAsSeq(p1, p2);
                    
                }
                return new GSharpUndefined();
            }
            double A = Math.Pow(m, 2) + 1;
            double B = (-2 * circle.Center.X) + (2 * line1.P1.Y * m) - (2 * Math.Pow(m, 2) * line1.P1.X) - (2 * m * circle.Center.Y);
            double C = Math.Pow(circle.Center.X, 2) + Math.Pow(line1.P1.Y, 2) - 2 * line1.P1.Y * m * line1.P1.X + Math.Pow(m, 2) * Math.Pow(line1.P1.X, 2)
                - 2 * line1.P1.Y * circle.Center.Y + 2 * m * line1.P1.X * circle.Center.Y + Math.Pow(circle.Center.Y, 2) - Math.Pow(circle.Radius, 2);
            double Discriminante = Math.Pow(B, 2) - 4 * A * C;
            if (Discriminante == 0)
            {
                GSharpPoint p1 = new GSharpPoint();
                p1.X = (-1 * B) / (2 * A);
                p1.Y = m * (p1.X - line1.P1.X) - line1.P1.Y;

               return line1.pointAsSeq(p1);
            }
            if (Discriminante > 0)
            {
                GSharpPoint p1 = new GSharpPoint();
                GSharpPoint p2 = new GSharpPoint();
               double raiz = Math.Sqrt(Discriminante);
                p1.X = (-1 * B + raiz) / (2 * A);
                 p1.Y = m * (p1.X - line1.P1.X) + line1.P1.Y;
                 p2.X = (-1 * B - raiz) / (2 * A);
                 p2.Y= m * (p2.X - line1.P1.X) + line1.P1.Y;

                return line1.pointAsSeq(p1, p2);
            }
            return new GSharpUndefined();
           
        }
        public static GSharpObject intersectsWithSegment(GSharpLine line1, GSharpSegment segm)
        {
            GSharpPoint p1 = new GSharpPoint();
            var m1 = CalculatePend(line1.P1, line1.P2);
            var m2 = CalculatePend(segm.P1, segm.P2);
            p1.X = (segm.P2.Y - line1.P2.Y + (m1 * line1.P2.X) - (m2 * segm.P2.X)) / (m1 - m2);
            p1.Y = -1 * (m1 * (line1.P1.X - p1.X) - line1.P1.Y);
            if (segm.P1.X <= p1.X && p1.X <= segm.P2.X) return line1.pointAsSeq(p1);
            if (segm.P2.X <= p1.X && p1.X <= segm.P1.X) return line1.pointAsSeq(p1);
            return new GSharpUndefined();
        }
        public static GSharpObject intersectsWithRay(GSharpLine line1, GSharpRay ray)
        {
            GSharpPoint p1 = new GSharpPoint();
            var m1 = CalculatePend(line1.P1, line1.P2);
            var m2 = CalculatePend(ray.P1, ray.P2);
            p1.X = (ray.P2.Y - line1.P2.Y + (m1 * line1.P2.X) - (m2 * ray.P2.X)) / (m1 - m2);
            p1.Y = -1 * (m1 * (line1.P1.X - p1.X) - line1.P1.Y);
            if (ray.P1.X < ray.P2.X && p1.X > ray.P1.X) return line1.pointAsSeq(p1);
            if (ray.P1.X >= ray.P2.X && p1.X <= ray.P1.X) return line1.pointAsSeq(p1);
          
            return new GSharpUndefined();
        }


    }
}
