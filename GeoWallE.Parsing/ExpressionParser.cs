using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace GeoWallE.Parsing
{
    public abstract class ExpressionParser
    {
        public abstract int Precedence { get; }

        public abstract bool Match(TokenConsumer tokenConsumer);

        public abstract ExpressionNode Parse(TokenConsumer tokenConsumer);
    }
}
