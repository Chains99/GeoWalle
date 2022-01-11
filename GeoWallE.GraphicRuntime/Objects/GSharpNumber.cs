using System;
using GeoWallE.GraphicRuntime.Types;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using GeoWallE.GraphicRuntime.Types.Interfaces_booleanas;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpNumber : GSharpObject, ISummable,IDivisible,ISubstractionable,IMultiplicationable,IMod,IEquals,IGreater,IGreaterOrEquals,ILess,ILessOrEquals,INotEquals,IAnd,IOr,INot
    {
        private static NumberType _Type = new NumberType();

        public double Value { get; set; }

        public override GSharpType Type
        {
            get { return _Type; }
        }

        public override string ToString()
        {
            return $"[Number - {Value}]";
        }

        public GSharpObject SumResult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpNumber();
            n.Value = this.Value + right.Value;
            return n;
        }

        public GSharpObject DivResult  (GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpNumber();
            n.Value = this.Value / right.Value;
            return n;
        }
        public GSharpObject MultResult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpNumber();
            n.Value = this.Value * right.Value;
            return n;
        }

        public GSharpObject SubResult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpNumber();
            n.Value = this.Value - right.Value;
            return n;
        }
        public GSharpObject ModResult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
            var n = new GSharpNumber();
            n.Value = this.Value % right.Value;
            return n;
        }

        

        public GSharpObject Equalsresult(GSharpObject rightType)
        {
            var right = rightType as GSharpNumber;
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
            var right = rightType as GSharpNumber;
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
            var right = rightType as GSharpNumber;
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
            var right = rightType as GSharpNumber;
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
            var right = rightType as GSharpNumber;
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
            var right = rightType as GSharpNumber;
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

        public int AndResult(GSharpObject rightType)
        {
            throw new NotImplementedException();
        }

        public int OrResult(GSharpObject rightType)
        {
            throw new NotImplementedException();
        }

        public int NotResult(GSharpObject rightType)
        {
            throw new NotImplementedException();
        }
    }
}
