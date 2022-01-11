using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class Print:Commands
    {
        Expression exp;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Print)
                return false;
            if (!context.Next())
                return false;
            exp = new Expression();

            return exp.build(context);
        }
        public override GSharpObject eval(Scope scope)
        {
            var v = exp.eval(scope);
            program.output.Text += v.ToString() +"\n";
            return null;
        }
    }

}
