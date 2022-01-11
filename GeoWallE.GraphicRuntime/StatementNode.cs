namespace GeoWallE.GraphicRuntime
{
    public abstract class StatementNode : GSharpNode
    {
        public abstract void Execute(GSharpExecutionContext executionContext);
    }
}
