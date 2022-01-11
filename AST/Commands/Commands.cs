using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    public class Commands:Expression
    {
        Commands command;
        public override bool build(TokenConsumer context)
        {
            command = new Draw();
            if (Verify(context))
              return true;
            command = new ColorC();
            if (Verify(context))
             return true; 
            command = new Restore();
            if (Verify(context))
             return true;         

            command = new Print();
            if (Verify(context))
                return true;

            return false;

        }
        private bool Verify(TokenConsumer context)
        {

            int pos = context.getPosition();
            if (!command.build(context))
            {
                context.setPosition(pos);
                return false;
            }
            return true;
        }
        public override GSharpObject eval(Scope scope)
        {
            return command.eval(scope);
        }


    }
}
