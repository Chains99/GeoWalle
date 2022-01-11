using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using GeoWallE.GraphicRuntime.Types.Interfaces_booleanas;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpMeasure : GSharpObject, IDivisible, IMultiplicationable, ISummable, ISubstractionable, IEquals, ILess, ILessOrEquals, IGreater, IGreaterOrEquals, INotEquals
    {
        private double v;

        public double Value { get; private set; }
        public GSharpMeasure(GSharpPoint P1, GSharpPoint P2)
        {
            Value = Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2));
        }
        public GSharpMeasure(double v)
        {
            Value = v;
        }

        public override GSharpType Type
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public GSharpObject SumResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpMeasure(v = 0);
            n.Value = this.Value + right.Value;
            return n;
        }

        public GSharpObject DivResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            n.Value = Value / right.Value;
            return n;
        }
        public GSharpObject MultResult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpMeasure(v = 1);
            n.Value = this.Value * Math.Abs( (int)right.Value);
            return n;
        }

        public GSharpObject SubResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpMeasure(v = 0);
            n.Value = Math.Abs( this.Value - right.Value);            
            return n;
        }

        public GSharpObject Equalsresult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value == right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }

        }

        public GSharpObject GreaterResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value > right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }
        }

        public GSharpObject GorE(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value >= right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }
        }

        public GSharpObject LesResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value < right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }
        }

        public GSharpObject LOrEResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value <= right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }
        }

        public GSharpObject NotEqualsResult(GSharpObject rightType)
        {
            var right = rightType as GSharpMeasure;
            var n = new GSharpNumber();
            if (this.Value != right.Value)
            {
                n.Value = 1;
                return n;
            }
            else
            {
                n.Value = 0;
                return n;
            }
        }
        public override GSharpNumber TrueOrFalse()
        {
            var a = new GSharpNumber();
            if (Value == 0)
            {
                a.Value = 0;
                return a;
            }
            else
            {
                a.Value = 1;
                return a;
            }
        }

        public override string ToString()
        {
            return $"[Measure - {Value}]";
        }

    }
}
