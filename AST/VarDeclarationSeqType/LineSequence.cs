﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoWallE.Parsing.LexicalAnalysis;

namespace AST
{
    class LineSequence:VarDeclarationSeqType
    {
        string id;
        public override bool build(TokenConsumer context)
        {
            if (context.Current.Value != TokenValues.Line)
                return false;
            if (!context.Next())
                return false;
            if (context.Current.Value != TokenValues.Sequence)
                return false;
            if (!context.Next())
                return false;
            if (context.Current.Type != TokenType.Identifier)
                return false;
            id = context.Current.Value;
            if (!context.Next())
                return false;
            return true;
        }
    }
}
