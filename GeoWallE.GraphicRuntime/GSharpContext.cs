using System;
using System.Collections.Generic;

namespace GeoWallE.GraphicRuntime
{
    public class GSharpContext
    {
        private readonly Dictionary<string, GSharpType> _memory;

        public GSharpContext()
        {
            _memory = new Dictionary<string, GSharpType>();
        }

        public void AddVariable(string name, GSharpType type)
        {
            _memory.Add(name, type);
        }

        public bool ContainsVariable(string name)
        {
            return _memory.ContainsKey(name);
        }

        public GSharpType GetValue(string name)
        {
            GSharpType type;
            if(_memory.TryGetValue(name, out type))
                return type;

            return null;
        }

        public GSharpContext CreateChildContext()
        {
            throw new Exception();
        }
    }
}
