namespace GeoWallE.GraphicRuntime.Types.Interfaces
{
    public interface IMultiplicationable
    {
        GSharpObject MultResult(GSharpObject rightType);
    }

    public static class MultExtensions
    {
        public static bool CanSum(this IMultiplicationable multip, GSharpType rightType)
        {
            return false;
            //return multip.Result(rightType) != null;
        }
    }
}

