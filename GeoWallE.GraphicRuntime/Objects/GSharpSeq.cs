using GeoWallE.GraphicRuntime.Types;
using GeoWallE.GraphicRuntime.Types.Interfaces;
using System.Collections.Generic;
using System;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpSeq : GSharpObject ,ISummable, IDrawable
    {
        private static SequenceType _Type = new SequenceType();

        int start = 0;
        private IEnumerable<GSharpObject> v;
        public IEnumerable<GSharpObject> values
        {
            get
            {
                int index = 0;
                foreach (var item in v)
                {
                    if (index >= start)
                        yield return item;
                    index++;
                }
            }
            set
            {
                this.v = value;
            }
        }         

        public void setPos(int pos)
        {
            this.start = pos;
        }
        public GSharpObject first()
        {
            foreach (var item in values)
            {
                return(GSharpPoint) item;
            }
            return new GSharpUndefined();
        }
        public GSharpObject Second()
        {
            bool sec = false;
            foreach (var item in values)
            {
                if(sec)
                  return item;
                sec = true;
            }
            return new GSharpUndefined();
        }
        public override GSharpType Type
        {
            get { return _Type; }
        }
        public bool isInfinite = false;

        public override string ToString()
        {
            if (this.isInfinite)
                return "Secuencia infinita";
            var v = "{";
            foreach (var item in values)
            {
                v += item.ToString() + ",";
            }
            return v.Substring(0, v.Length - 1)+"}";
        }

        public override GSharpNumber TrueOrFalse()
        {
            var a = new GSharpNumber();
            var r = false;
            foreach(var i in values)
            {
                r = true;
                break;
            }
            if (!r)
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

        public void Draw(GDrawer drawer)
        {
            throw new NotImplementedException();
        }

        public GSharpObject SumResult(GSharpObject rightType)
        {
            var a = new GSharpSeq();
            a.values = sum(this, rightType);           
            return a;
        }

        public IEnumerable<GSharpObject> sum(GSharpSeq seq, GSharpObject obj)
        {
            foreach (var item in seq.values)
            {
                yield return item;
            }
            if(obj is GSharpSeq)
            {
                var o = obj as GSharpSeq;
                foreach (var item in o.values)
                {
                    yield return item;
                }
            }
        }

        public void Draw(GDrawer drawer, string label)
        {
            if (isInfinite)
                return;
            foreach (IDrawable item in values)
            {
                item.Draw(drawer, label);
            }
        }
        public override bool IsDrawable
        {
            get
            {
                foreach (var item in this.v)
                {
                    return item.IsDrawable;
                }
                return false;
            }
        }
    }
}
