using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.GraphicRuntime.Core;
using GeoWallE.GraphicRuntime;

namespace AST
{
    public class ColorC:Commands
    {
        string[] colorA = { TokenValues.Red, TokenValues.Blue, TokenValues.Magenta, TokenValues.Yellow, TokenValues.White, TokenValues.Gray, TokenValues.Green, TokenValues.Cyan, TokenValues.Black };
        string color;

        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Color)
                return false;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.CurrentPrev.Location, ErrorCode.Expected, "Comando color esperaba un siguiente "));
                return false;
            }
            if (Array.IndexOf(colorA,context.Current.Value)==-1)
            {
                program.errors.AddError(new CompilingError(context.Current.Location, ErrorCode.Invalid, "Comando color esperaba un color predefinido."));
                return false;
            }
            color = context.Current.Value;
            if (!context.Next())
            {
                program.errors.AddError(new CompilingError(context.CurrentPrev.Location, ErrorCode.Expected, "Comando color no completado."));
                return false;
            }
            
            return true;
        }
        public override GSharpObject eval(Scope scope)
        {           
            switch (color)
            {
                case TokenValues.Red:
                    SetColor(Color.Red);
                    break;
                case TokenValues.Blue:
                    SetColor(Color.Blue);
                    break;
                case TokenValues.Cyan:
                   SetColor(Color.Cyan);
                    break;
                case TokenValues.Yellow:
                    SetColor(Color.Yellow);
                    break;
                case TokenValues.White:
                    SetColor(Color.White);
                    break;
                case TokenValues.Green:
                    SetColor(Color.Green);
                    break;
                case TokenValues.Gray:
                    SetColor(Color.Gray);
                    break;
                case TokenValues.Black:
                    SetColor(Color.Black);
                    break;
                case TokenValues.Magenta:
                    SetColor(Color.Magenta);
                    break;
            }
            return null;
        }
        public static Stack<Color> colors = new Stack<Color>();
        void SetColor(Color color)
        {
            colors.Push(program.drawer.getColor());
            program.drawer.SetColor(color);
        }       
    }
}
