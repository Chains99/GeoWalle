namespace GeoWallE.GraphicRuntime.Types.Interfaces
{
    public interface ISummable
    {
        GSharpObject SumResult(GSharpObject rightType);
    }

    public static class SummableExtensions
    {
        public static bool CanSum(this ISummable summable, GSharpType rightType)
        {     
            return false;
            //return summable.Result(rightType) != null;
        }
    }
}
