namespace GeoWallE.GraphicRuntime.Types.Interfaces
{
    public interface IMod
    {
        GSharpObject ModResult(GSharpObject rightType);
    }

    public static class ModExtensions
    {
        public static bool CanSum(this IMod mod, GSharpObject rightType)
        {
            return false;
            //return mod.Result(rightType) != null;
        }
    }
}

