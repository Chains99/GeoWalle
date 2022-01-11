using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime.Types.Interfaces;  

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpArc : GSharpObject,IDrawable,Iintersect
    {
        public override GSharpType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public GSharpPoint P1 { get; set; }
        public GSharpPoint Center { get; set; }
        public GSharpPoint P2 { get; set; }

        public double Radius { get; set; }
        public GSharpArc(GSharpPoint a, GSharpPoint b, GSharpPoint c, double radius)
        {
            Center= a;
            P1 = b;
            P2 = c;
            Radius = radius;
        }
        public GSharpArc() { }

        public override string ToString()
        {
            return $"[Arc - Center{Center}, P1: {P1}, P2: {P2}, Radius:{Radius}]";
        }

        public void Draw( GDrawer drawer, string label)
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
                return GSharpArc.intersectWithAll(this, obj as GSharpPoint);
            if (obj is GSharpLine)
                return GSharpArc.intersectWithAll (this,obj as GSharpLine);
            if (obj is GSharpCircle)
                return GSharpArc.intersectWithAll(this, obj as GSharpCircle);
            if (obj is GSharpSegment)
                return GSharpArc.intersectWithAll(this, obj as GSharpSegment);
            if (obj is GSharpRay)
                return GSharpArc.intersectWithAll(this,obj as GSharpRay);
            if (obj is GSharpArc)
                return GSharpArc.intersectWithAll(this, obj as GSharpArc);
            return new GSharpUndefined();
          
        }
        public static GSharpObject intersectWithAll(GSharpArc arc1,GSharpObject obj)
        {
          
            GSharpCircle circle = new GSharpCircle(arc1.Center, arc1.Radius);
           var points = circle.intersect(obj);

            if (points is GSharpUndefined) return points;


            var ray = new GSharpRay(arc1.Center, arc1.P2);
            GSharpSeq begin =(GSharpSeq) ray.intersect(circle);
            var ray1 = new GSharpRay(arc1.Center, arc1.P1);
            GSharpSeq end =(GSharpSeq) ray1.intersect(circle);
            GSharpPoint first = (GSharpPoint)(end.first());
            double startangle = AngleBetween(arc1.Center, (GSharpPoint)(((GSharpSeq)begin).first()));
            double endangle = AngleBetween(arc1.Center, (GSharpPoint)(((GSharpSeq)end).first()));
            GSharpPoint[] p = new GSharpPoint[2];
            int pos = 0;
            foreach (GSharpPoint a in ((GSharpSeq)(points)).values)
            {
                double angulo = AngleBetween(arc1.Center, a);
                if (startangle > endangle)
                {
                    if (angulo < startangle && angulo > endangle) { p[pos] = a; pos++; };
                }
                else
                {
                    if ((angulo < startangle && angulo < endangle) || (angulo > startangle && angulo > endangle))
                    {
                        p[pos] = a;
                        pos++;
                    }
                }
                
            }
            return arc1.pointAsSeq(p);
        }
        public static double AngleBetween(GSharpPoint Centre, GSharpPoint point)
        {
          
            int Cuadrante = 0;
            Cuadrante = GSharpArc.Cuadrante(Centre, point);
            double DeltaX1 = point.X - Centre.X;
            double DeltaY1 = point.Y - Centre.Y;
            double Hipotenuse1 = Math.Sqrt(Math.Pow(DeltaX1, 2) + Math.Pow(DeltaY1, 2));
            double x = Math.Asin(Math.Abs(DeltaY1) / Hipotenuse1);
            if (Cuadrante == 2) x = Math.PI - x;
            if (Cuadrante == 3) x += Math.PI;
            if (Cuadrante == 4) x = (2 * Math.PI) - x;
            return (x * 180) / Math.PI;
        }
        private static int Cuadrante(GSharpPoint centro, GSharpPoint p2)
        {
            if (centro.Y == p2.Y)
            {
                if (p2.X > centro.X) return 1;
                return 3;
            }
            if (centro.X == p2.X)
            {
                if (p2.Y > centro.Y) return 4;
                return 2;
            }
            if (centro.Y < p2.Y)
            {
                if (centro.X < p2.X) return 1;
                else return 2;
            }
            if (centro.X < p2.X) return 4;
            else return 3;
        }
    }
}
