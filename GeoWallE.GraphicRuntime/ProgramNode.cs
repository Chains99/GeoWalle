using GeoWallE.GraphicRuntime.Core;
using System.Collections.Generic;

namespace GeoWallE.GraphicRuntime
{
    public class ProgramNode
    {
        private readonly List<StatementNode> _statements;
        private bool _isOk;

        public ProgramNode(IEnumerable<StatementNode> statements)
        {
            _statements = new List<StatementNode>(statements);
            _isOk = false;
        }

        public List<StatementNode> Statements
        {
            get { return _statements; }
        }

        public bool IsOk
        {
            get { return _isOk; }
        }

        public void CheckSemantics(GSharpContext context, OutputInfo outputInfo)
        {
            if (_statements == null)
                return;

            _isOk = true;
            foreach (var statement in _statements)
            {
                statement.CheckSemantics(context, outputInfo);
                _isOk &= statement.IsOk;
            }
        }

        public void Run(GSharpExecutionContext executionContext)
        {
            foreach (var statement in _statements)
            {
                statement.Execute(executionContext);
            }
        }
    }
}
