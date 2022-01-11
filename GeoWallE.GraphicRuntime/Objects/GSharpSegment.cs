using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpSegment : GSharpObject, IDrawable, Iintersect
    {


        public GSharpPoint P1 { get; set; }

        public GSharpPoint P2 { get; set; }

        public override GSharpType Type
        {
            get { return null; }
        }

        public void Draw(GDrawer drawer, string label)
        {
            drawer.Draw(this, label);
        }

        public GSharpLine GetLine()
        {
            var l = new GSharpLine();
            l.P1 = P1;
            l.P2 = P2;
            return l;
        }



        public override string ToString()
        {
            return $"[Segment - P1: {P1}, P2: {P2}]";
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
                return GSharpPoint.intersectsWithSegment(obj as GSharpPoint, this);
            if (obj is GSharpLine)
                return GSharpLine.intersectsWithSegment(obj as GSharpLine, this);
            if (obj is GSharpCircle)
                return GSharpCircle.intersectWithSegmt(this, obj as GSharpCircle);
            if (obj is GSharpSegment)
                return GSharpSegment.intersectWithSegmt(this, obj as GSharpSegment);
            if (obj is GSharpRay)
                return intersectWithRay(obj as GSharpRay, this);
            if (obj is GSharpArc)
                return GSharpArc.intersectWithAll(obj as GSharpArc, this);
            return new GSharpUndefined();
        }

        public static GSharpObject intersectWithRay(GSharpRay ray, GSharpSegment segmt)
        {
            var m2 = CalculatePend(ray.P1, ray.P2);
            var m1 = CalculatePend(segmt.P1, segmt.P2);
            GSharpPoint p1 = new GSharpPoint();
            p1.X = (segmt.P2.Y - ray.P2.Y + (m2 * ray.P2.X) - (m1 * segmt.P2.X)) / (m2 - m1);
            p1.Y = -1 * (m2 * (ray.P1.X - p1.X) - ray.P1.Y);

            if (segmt.P1.X <= p1.X && p1.X <= segmt.P2.X)
                if ((ray.P1.X <= p1.X && ray.P2.X >= ray.P1.X)  ||(ray.P1.X >= p1.X && ray.P1.X >= ray.P2.X))
                    return segmt.pointAsSeq(p1);
            if (segmt.P2.X <= p1.X && p1.X <= segmt.P1.X)
                if ((ray.P1.X <= p1.X && ray.P2.X >= ray.P1.X) || (ray.P1.X >= p1.X && ray.P1.X >= ray.P2.X))
                    return segmt.pointAsSeq(p1);

            return new GSharpUndefined();
        }

        public static GSharpObject intersectWithSegmt(GSharpSegment segmt1, GSharpSegment segmt2)
        {
            var m1 = CalculatePend(segmt1.P1, segmt1.P2);
            var m2 = CalculatePend(segmt2.P1, segmt2.P2);
            if (m1 == m2)
            {
                return new GSharpUndefined();
            }
            GSharpPoint p1 = new GSharpPoint();
            p1.X = (segmt2.P2.Y - segmt1.P2.Y + (m1 * segmt2.P2.X) - (m2 * segmt2.P2.X)) / (m1 - m2);
            p1.Y = -1 * (m1 * (segmt1.P1.X - p1.X) - segmt1.P1.Y);

            //if (!(segmt1.P1.X <= p1.X && p1.X <= segmt1.P2.X) && !(segmt1.P2.X <= p1.X && p1.X <= segmt1.P1.X))
            //    return new GSharpUndefined();

            if (segmt2.P1.X <= p1.X && p1.X <= segmt2.P2.X) return segmt1.pointAsSeq(p1);
            if (segmt2.P2.X <= p1.X && p1.X <= segmt2.P1.X) return segmt1.pointAsSeq(p1);


            return new GSharpUndefined();

        }
        static double CalculatePend(GSharpPoint p1, GSharpPoint p2)
        {
            return (p1.Y - p2.Y) / (p1.X - p2.X);
        }
    }
}
