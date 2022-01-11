namespace GeoWallE.GraphicRuntime.Core
{
    public class CompilingError
    {
        public CodeLocation Location { get; private set; }
        public ErrorCode Code { get; private set; }

        public string Argument { get; private set; }

        public CompilingError(CodeLocation location, ErrorCode code, string argument)
        {
            this.Location = location;
            this.Code = code;
            this.Argument = argument;
        }
    }
}
