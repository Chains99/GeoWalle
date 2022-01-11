using System.Collections.Generic;

namespace GeoWallE.GraphicRuntime.Core
{
    public class OutputInfo
    {
        private readonly List<CompilingError> _errors;

        public OutputInfo()
        {
            _errors = new List<CompilingError>();
        }

        public void AddError(CompilingError error)
        {
            _errors.Add(error);
        }

        public void AddUnexpectedTokenError(CodeLocation location, string token)
        {
            _errors.Add(new CompilingError(location, ErrorCode.Expected, token));
        }

        public IEnumerable<CompilingError> Errors
        {
            get { return _errors; }
        }
    }
}
