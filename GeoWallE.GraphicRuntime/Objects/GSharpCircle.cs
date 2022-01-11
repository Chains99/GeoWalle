using System;
using GeoWallE.GraphicRuntime.Types;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using System.Collections.Generic;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpCircle : GSharpObject, IDrawable, Iintersect
    {
        private static CircleType _Type = new CircleType();

        public GSharpPoint Center { get; set; }

        public double Radius { get; set; }

        public override GSharpType Type
        {
            get { return _Type; }
        }
        public GSharpCircle()
        { }
        public GSharpCircle(GSharpPoint p1, double r)
        {
            Center = p1;
            Radius = r;
        }
        public override string ToString()
        {
            return $"[Circle - Center: {Center}, Radius: {Radius}]";
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
                return GSharpPoint.intersectsWithCircle(obj as GSharpPoint, this);
            if (obj is GSharpLine)
                return GSharpLine.intersectsWithCircle(obj as GSharpLine, this);
            if (obj is GSharpCircle)
                return intersectWithCircle(this, obj as GSharpCircle);
            if (obj is GSharpSegment)
                return GSharpCircle.intersectWithSegmt(obj as GSharpSegment, this);
            if( obj is GSharpRay)
                return intersectWithRay(obj as GSharpRay, this);
            if (obj is GSharpArc)
                return GSharpArc.intersectWithAll(obj as GSharpArc, this);
            return new GSharpUndefined();
        }

       

        public static GSharpObject intersectWithCircle(GSharpCircle circle1,GSharpCircle circle2)
        {
            
            double[] CoeficientesSuma = {0,2*circle2.Center.X-2*circle1.Center.X,Math.Pow(circle1.Center.X,2)-Math.Pow(circle2.Center.X,2),
            0,2*circle2.Center.Y-2*circle1.Center.Y,Math.Pow(circle1.Center.Y,2)-Math.Pow(circle2.Center.Y,2),Math.Pow(circle2.Radius,2)-Math.Pow(circle1.Radius,2)};
            double[] CreateLine = new double[2];
            CreateLine[0] = -1 * (CoeficientesSuma[4] / CoeficientesSuma[1]);
            CreateLine[1] = -1 * (CoeficientesSuma[2] + CoeficientesSuma[5] + CoeficientesSuma[6]) / CoeficientesSuma[1];
            GSharpPoint P1 = new GSharpPoint();
            GSharpPoint P2 = new GSharpPoint();
            P1.X = CreateLine[1];
            P1.Y = 0;
            P2.X = CreateLine[0] * 1000 + CreateLine[1];
            P2.Y = 1000;
            GSharpLine line = new GSharpLine(P1, P2);
            return GSharpLine.intersectsWithCircle(line, circle1);
           
        }
        public static GSharpObject intersectWithSegmt(GSharpSegment segmt, GSharpCircle circle)
        {
            var m = CalculatePend(segmt.P1, segmt.P2);
            GSharpPoint p1 = new GSharpPoint();
            GSharpPoint p2 = new GSharpPoint();
            if (m == double.MaxValue)
            {
                if (segmt.P1.X < circle.Center.X + circle.Radius && segmt.P1.X > circle.Center.X - circle.Radius)
                {
                    double side = Math.Sqrt(Math.Pow(circle.Radius, 2) - Math.Pow(Math.Abs(circle.Center.X - segmt.P1.X), 2));
                    p1.X = segmt.P1.X;
                    p1.Y = circle.Center.Y + side;
                    p2.X = segmt.P1.X;
                    p2.Y = circle.Center.Y - side;
                    return circle.pointAsSeq(p1, p2);
                }
                return new GSharpUndefined();
            }
            double A = Math.Pow(m, 2) + 1;
            double B = (-2 * circle.Center.X) + (2 * segmt.P1.Y * m) - (2 * Math.Pow(m, 2) * segmt.P1.X) - (2 * m * circle.Center.Y);
            double C = Math.Pow(circle.Center.X, 2) + Math.Pow(segmt.P1.Y, 2) - 2 * segmt.P1.Y * m * segmt.P1.X + Math.Pow(m, 2) * Math.Pow(segmt.P1.X, 2)
                - 2 * segmt.P1.Y * circle.Center.Y + 2 * m * segmt.P1.X * circle.Center.Y + Math.Pow(circle.Center.Y, 2) - Math.Pow(circle.Radius, 2);
            double Discriminante = Math.Pow(B, 2) - 4 * A * C;
            if (Discriminante < 0) return new GSharpUndefined();
           
            p1.X = 0;
            p1.Y = 0;
            p2.X = 0;
            p2.Y = 0;
            if (Discriminante == 0)
            {
              
                p1.X = (-1 * B) / (2 * A);
                p1.Y = m * (p1.X - segmt.P1.X) - segmt.P1.Y;
           
                if ((segmt.P1.X <= p1.X && p1.X <= segmt.P2.X) || (segmt.P2.X <= p1.X && p1.X <= segmt.P1.X)) return circle.pointAsSeq(p1);
            }
            if (Discriminante > 0)
            {
               
                double raiz = Math.Sqrt(Discriminante);
                p1.X = (-1 * B + raiz) / (2 * A);
                p1.Y = m * (p1.X - segmt.P1.X) + segmt.P1.Y;
                p2.X = (-1 * B - raiz) / (2 * A);
                p2.Y = m * (p2.X - segmt.P1.X) + segmt.P1.Y;
                bool p = !(segmt.P1.X <= p1.X && p1.X <= segmt.P2.X) && !(segmt.P2.X <= p1.X && p1.X <= segmt.P1.X);
                bool q = !(segmt.P1.X <= p2.X && p2.X <= segmt.P2.X) && !(segmt.P2.X <= p2.X && p2.X <= segmt.P1.X);

                if (!p && q)
                    return circle.pointAsSeq(p1);
                if (p && !q)
                    return circle.pointAsSeq(p2);
                if(!p&&!q)
                {
                    return circle.pointAsSeq(p2,p1);
                }
                
            }
            return new GSharpUndefined();
        }
        public static GSharpObject intersectWithRay(GSharpRay ray, GSharpCircle circle)
        {
            GSharpPoint p1 = new GSharpPoint();
            GSharpPoint p2 = new GSharpPoint();
            var m = CalculatePend(ray.P1, ray.P2);
            List<IDrawable> intersects = new List<IDrawable>();
            if (m == double.MaxValue)
            {
                if (ray.P1.X < circle.Center.X + circle.Radius && ray.P1.X > circle.Center.X - circle.Radius)
                {
                    double side = Math.Sqrt(Math.Pow(circle.Radius, 2) - Math.Pow(Math.Abs(circle.Center.X - ray.P1.X), 2));
                    p1.X = ray.P1.X;
                    p1.Y= circle.Center.Y + side;
                    p2.X = ray.P1.X;
                    p2.Y = circle.Center.Y - side;
                }
                return new GSharpUndefined();
            }
            double A = Math.Pow(m, 2) + 1;
            double B = (-2 * circle.Center.X) + (2 * ray.P1.Y * m) - (2 * Math.Pow(m, 2) * ray.P1.X) - (2 * m * circle.Center.Y);
            double C = Math.Pow(circle.Center.X, 2) + Math.Pow(ray.P1.Y, 2) - 2 * ray.P1.Y * m * ray.P1.X + Math.Pow(m, 2) * Math.Pow(ray.P1.X, 2)
                - 2 * ray.P1.Y * circle.Center.Y + 2 * m * ray.P1.X * circle.Center.Y + Math.Pow(circle.Center.Y, 2) - Math.Pow(circle.Radius, 2);
            double Discriminante = Math.Pow(B, 2) - 4 * A * C;
            if (Discriminante < 0) return new GSharpUndefined();
            p1.X = 0;
            p1.Y= 0;
            p2.X = 0;
            p2.Y = 0;
            if (Discriminante == 0)
            {
               
                p1.X = (-1 * B) / (2 * A);
                p1.Y= m * (p1.X - ray.P1.X) - ray.P1.Y;

                return circle.pointAsSeq(p1);
            }
            if (Discriminante > 0)
            {
                double raiz = Math.Sqrt(Discriminante);

                p1.X = (-1 * B + raiz) / (2 * A);
                p1.Y = m * (p1.X - ray.P1.X) + ray.P1.Y;
                p2.X = (-1 * B - raiz) / (2 * A);
                p2.Y = m * (p2.X - ray.P1.X) + ray.P1.Y;

                intersects.Add(p1);
                intersects.Add(p2);

                bool p = !(ray.P1.X <= p1.X && ray.P2.X >= ray.P1.X) && !(ray.P1.X >= p1.X && ray.P1.X >= ray.P2.X);
                bool q = !(ray.P1.X <= p2.X && ray.P2.X >= ray.P1.X) && !(ray.P1.X >= p2.X && ray.P1.X >= ray.P2.X);
                if (p && q)
                {
                    return new GSharpUndefined();
                }
                if (!p && !q)
                    return circle.pointAsSeq(p1, p2);
                if (p)
                    return circle.pointAsSeq(p2);
                return circle.pointAsSeq(p1);
            }

                
            
            return new GSharpUndefined();
        }
        static double CalculatePend(GSharpPoint p1, GSharpPoint p2)
        {
            return (p1.Y - p2.Y) / (p1.X - p2.X);
        }
    }
}
