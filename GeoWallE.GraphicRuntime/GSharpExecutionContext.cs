using System;

namespace GeoWallE.GraphicRuntime
{
    public class GSharpExecutionContext
    {
        private readonly GDrawer _drawer;
        private readonly GPrinter _printer;

        public GSharpExecutionContext(GDrawer drawer, GPrinter printer)
        {
            _drawer = drawer;
            _printer = printer;
        }

        public GDrawer Drawer
        {
            get { return _drawer; }
        }

        public GPrinter Printer
        {
            get { return _printer; }
        }

        public void RegisterVariable(string name, GSharpObject value)
        {
            throw new NotImplementedException();
        }

        public GSharpObject GetVariable(string name)
        {
            throw new NotImplementedException();
        }
    }
}
