namespace GeoWallE.GraphicRuntime.Types.Interfaces
{
    public interface IDivisible
    {
        GSharpObject DivResult(GSharpObject rightType);
    }

    public static class DivisibleExtensions
    {
        public static bool CanSum(this IDivisible divisible, GSharpType rightType)
        {
            return false;
            //return divisible.Result(rightType) != null;
        }
    }
}
