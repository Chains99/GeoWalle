using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWallE
{
    /// <summary>
    /// Persistent drawing canvas via an image. Scaling this image might produce aliased results.
    /// Try to improve this project using your objects and managing an internal collection of drawable objects rather than an image.
    /// </summary>
    public partial class Canvas : UserControl
    {
        public Canvas()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            SetBackBuffer(128, 128);
            _color = Color.Black;
        }

        Graphics _gr;
        Bitmap _backBuffer;
        Color _color;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_backBuffer, 0, 0);
        }

        private void SetBackBuffer(int width, int height)
        {
            _backBuffer = new Bitmap(width, height);
            _gr = Graphics.FromImage(_backBuffer);
            _gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            _gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            _gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _gr.PageUnit = GraphicsUnit.Pixel;
        }
       

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (ClientSize.Width > 0 && ClientSize.Height > 0)
                SetBackBuffer(this.ClientSize.Width, this.ClientSize.Height);
        }

        public void Clear (Color backColor)
        {
            _gr.Clear(backColor);
        }

        public void SetColor(Color color)
        {
            this._color = color;
        }
        public Color GetColor()
        {
            return this._color;
        }

        public void DrawPoint(PointF p, string label)
        {
            _gr.FillEllipse(new SolidBrush(_color), p.X - 4, p.Y - 4, 8, 8);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p.X + 6, p.Y - 6);
        }

        public void DrawLine(PointF p1, PointF p2, string label)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            PointF far1 = new PointF(p2.X + dx * 100, p2.Y + dy * 100);
            PointF far2 = new PointF(p1.X - dx * 100, p1.Y - dy * 100);
            _gr.DrawLine(new Pen(_color), far1, far2);
            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1.X + 6, p1.Y - 6);
        }

        public void DrawCircle(PointF center, double radius, string label)
        {
            _gr.DrawEllipse(new Pen(_color), center.X - (float)radius, center.Y - (float)radius, (float)radius * 2, (float)radius * 2);

            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, center.X + (float)radius + 4, center.Y);
        }

        public void DrawRay(PointF p1, PointF p2, string label)
        {
            //float dx = p2.X - p1.X;
            //float dy = p2.Y - p1.Y;
            //PointF far1 = new PointF(p2.X  , p2.Y + dy * 100);
            //PointF far2 = new PointF(p1.X, p1.Y - dy * 100);
            //_gr.DrawLine(new Pen(_color), far1, far2);
            //if (!string.IsNullOrEmpty(label))
            //    _gr.DrawString(label, Font, Brushes.Black, p1.X + 6, p1.Y - 6);
            var x = (p2.X - p1.X) * 1000;
            var y = (p2.Y - p1.Y) * 1000;

            var rayP = new PointF(x, y);
            _gr.DrawLine(new Pen(_color), p1, rayP);

            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1);
        }
        internal void DrawSegment(PointF p1, PointF p2, string label)
        {
            _gr.DrawLine(new Pen(_color), p1, p2);

            if (!string.IsNullOrEmpty(label))
                _gr.DrawString(label, Font, Brushes.Black, p1);
        }

        internal void DrawArc(Point center, Point start,Point end,int radius, string label)
        {
            //De derecha a izquierda como C#
            int Cinicio = Cuadrante(center, start);
            int Cfinal = Cuadrante(center, end);
            double anguloinicio = AngleBetween(center, start);
            double angulofinal = AngleBetween(center, end);

            if (Cinicio == 2) anguloinicio = Math.PI - anguloinicio;
            if (Cinicio == 3) anguloinicio += Math.PI;
            if (Cinicio == 4) anguloinicio = (2 * Math.PI) - anguloinicio;

            if (Cfinal == 2) angulofinal = Math.PI - angulofinal;
            if (Cfinal == 3) angulofinal += Math.PI;
            if (Cfinal == 4) angulofinal = (2 * Math.PI) - angulofinal;

            angulofinal *= (360 / (2 * Math.PI));
            anguloinicio *= (360 / (2 * Math.PI));
            float sweep = 0;
            if (Cinicio == Cfinal)
            {
                if (anguloinicio < angulofinal) sweep = (float)(angulofinal - anguloinicio);
                else sweep = 360 - (float)(anguloinicio - angulofinal);
            }
            else if (Cinicio < Cfinal)
            {
                sweep = (float)(angulofinal - anguloinicio);
            }
            else sweep = 360 - (float)(anguloinicio - angulofinal);

            _gr.DrawArc(new Pen(_color), (int)(center.X - radius ), (int)(center.Y - radius),
                radius * 2 , radius * 2 , (float)anguloinicio, sweep);
        }
        private int Cuadrante(Point centro, Point p2)
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
        private double AngleBetween(Point Centre, Point P)
        {
            double DeltaX1 = P.X - Centre.X;
            double DeltaY1 = P.Y - Centre.Y;
            double Hipotenuse1 = Math.Sqrt(Math.Pow(DeltaX1, 2) + Math.Pow(DeltaY1, 2));
            return Math.Asin(Math.Abs(DeltaY1) / Hipotenuse1);
        }
    }
}
