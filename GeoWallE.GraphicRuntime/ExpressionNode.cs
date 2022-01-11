namespace GeoWallE.GraphicRuntime
{
    public abstract class ExpressionNode : GSharpNode
    {
        public abstract GSharpType Type { get; }

        public override bool IsOk
        {
            get { return Type != null; }
        }

        public abstract GSharpObject Evaluate(GSharpExecutionContext executionContext);
    }
}
