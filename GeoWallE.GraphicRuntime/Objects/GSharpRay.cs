using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime.Types.Interfaces;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpRay : GSharpObject, IDrawable, Iintersect
    {
        public GSharpPoint P1 { get; set; }
        public GSharpPoint P2 { get; set; }

        public override GSharpType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public GSharpRay(GSharpPoint p1, GSharpPoint p2)
        {
            P1 = p1;
            P2 = p2;
        }
        public GSharpRay() { }

        public void Draw( GDrawer drawer, string label)
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

        public GSharpObject intersect(GSharpObject obj)
        {
            if (obj is GSharpPoint)
                return GSharpPoint.intersectsWithRay(obj as GSharpPoint, this);
            if (obj is GSharpLine)
                return GSharpLine.intersectsWithRay(obj as GSharpLine, this);
            if (obj is GSharpCircle)
                return GSharpCircle.intersectWithRay(this, obj as GSharpCircle);
            if (obj is GSharpSegment)
                return GSharpSegment.intersectWithRay(this, obj as GSharpSegment);
            if (obj is GSharpRay)
                return intersectWithRay(this, obj as GSharpRay);
            if (obj is GSharpArc)
                return GSharpArc.intersectWithAll(obj as GSharpArc, this);
          
            return new GSharpUndefined();
        }

      

        public override string ToString()
        {
            return $"[Ray - P1: {P1}, P2: {P2}]";
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
        public static GSharpObject intersectWithRay(GSharpRay ray1, GSharpRay ray2)
        {
            var m1 = CalculatePend(ray1.P1, ray1.P2);
            var m2 = CalculatePend(ray2.P1, ray2.P2);
            GSharpPoint p1 = new GSharpPoint();
            p1.X = (ray2.P2.Y - ray1.P2.Y + (m1 * ray1.P2.X) - (m2 * ray2.P2.X)) / (m1 - m2);
            p1.Y = -1 * (m1 * (ray1.P1.X - p1.X) - ray1.P1.Y);
            if ((ray2.P1.X <= p1.X && ray2.P2.X >= ray2.P1.X) || (ray2.P1.X >= p1.X && ray2.P1.X >= ray2.P2.X))
            {
                if (!(ray1.P1.X <= p1.X && ray1.P2.X >= ray1.P1.X) && !(ray1.P1.X >= p1.X && ray1.P1.X >= ray1.P2.X))
                    return new GSharpSeq();
                return ray1.pointAsSeq(p1);
            }
            return new GSharpUndefined(); 

          
            
        }
        static double CalculatePend(GSharpPoint p1, GSharpPoint p2)
        {
            return (p1.Y - p2.Y) / (p1.X - p2.X);
        }
    }
}
