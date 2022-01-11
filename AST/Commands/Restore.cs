using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Core;
using System.Drawing;


namespace AST
{
    class Restore : Commands
    {
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Restore)
                return false;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.CurrentPrev.Location, ErrorCode.Expected, "Comando restore no completado "));
                return false;
            }
            return true;
        }
        public override GSharpObject eval(Scope scope)
        {
            var c = Color.Black;
            if (ColorC.colors.Count > 0)
                c = ColorC.colors.Pop();
            program.drawer.SetColor(c);
            return null;
        }
    }
}
