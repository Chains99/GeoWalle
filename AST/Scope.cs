using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.GraphicRuntime;
using GeoWallE.GraphicRuntime.Objects;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    public class Scope
    {
        Scope parent;
        Dictionary<string, GSharpObject> vars;
        Dictionary<string, DeclarationFun> func;
        public Scope(Scope parent)
        {
            this.parent = parent;
            this.vars = new Dictionary<string, GSharpObject>();
            this.func = new Dictionary<string, DeclarationFun>();
        }

        public GSharpObject getVar(string key)
        {
            if (vars.ContainsKey(key))
                return vars[key];
            if (parent != null)
                return parent.getVar(key);
            return null;
        }
        public void setVar(string key, GSharpObject value)
        {
            //if (isKeyUsed(key))
            //    throw new Exception("Ya se estaba usando la llave");
            // underscore es un comodin y por tanto no guarda valor
            if (key == TokenValues._)
                return;
            //no se puede sobrescribir a una variable en un ambito padre pagina 11
            //esto solo se ve en el chequeo semantico
            if (!this.vars.ContainsKey(key) /*&& !this.parentHasVar(key)*/)
                this.vars.Add(key, value);
            //en el lenguaje solo hay constantes no variables.           
        }
        bool isKeyUsed(string key)
        {
            return vars.ContainsKey(key) || func.ContainsKey(key) || parentHasFunc(key) || parentHasVar(key);
        }
        bool parentHasVar(string key)
        {
            if (parent != null)
                return parent.getVar(key) != null;
            return false;
        }
        bool parentHasFunc(string key)
        {
            if (parent != null)
                return parent.getFunc(key) != null;
            return false;
        }
        public DeclarationFun getFunc(string key)
        {
            if (func.ContainsKey(key))
                return func[key];
            if (parent != null)
                return parent.getFunc(key);
            return null;
        }
        public void setFunc(string key, DeclarationFun value)
        {
            //if (isKeyUsed(key))
            //    throw new Exception("Ya se estaba usando la llave");
            this.func.Add(key, value);
        }

    }
}
