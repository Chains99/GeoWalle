using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;
using GeoWallE.Parsing;
using GeoWallE.GraphicRuntime;

namespace AST
{
    class Import:Commands
    {
        Token str;
        program p;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Import)
                return false;
            if (!context.Next())
                return false;
            str = context.Current;
            if (!buildImport())
                return false;

            if (!context.Next())
                return true;
            if (context.Current.Value == TokenValues.StatementSeparator)
                context.Next();

            return true;
        }
        bool buildImport()
        {
            if (!File.Exists(str.Value))
            {
                program.errors.AddError(new GeoWallE.GraphicRuntime.Core.CompilingError(str.Location, GeoWallE.GraphicRuntime.Core.ErrorCode.Invalid, "El archivo a importar no existe"));
                return false;
            }

            var code = File.ReadAllText(str.Value);
            p = new program();
            return p.build(program.lexer.GetTokenConsumer(str.Value, code));
        }
        public override GSharpObject eval(Scope scope)
        {
            p.eval(scope);
            return null;
        }
    }
}
