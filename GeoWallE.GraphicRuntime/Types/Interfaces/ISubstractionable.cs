namespace GeoWallE.GraphicRuntime.Types.Interfaces
{
    public interface ISubstractionable
    {
        GSharpObject SubResult(GSharpObject rightType);
    }

    public static class SubstracionableExtensions
    {
        public static bool CanSub(this ISubstractionable substr, GSharpObject rightType)
        {
            //return substr.Result(rightType) != null;
            return false;
        }
    }
}

