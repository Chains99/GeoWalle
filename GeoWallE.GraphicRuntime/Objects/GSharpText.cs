using GeoWallE.GraphicRuntime.Types;

namespace GeoWallE.GraphicRuntime.Objects
{
    public class GSharpText : GSharpObject
    {
        private static TextType _Type = new TextType();

        public string Value { get; set; }

        public override GSharpType Type
        {
            get { return _Type; }
        }

        public override string ToString()
        {
            return $"[String - {Value}]";
        }
    }
}
