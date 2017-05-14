// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace Microsoft.CodeAnalysis.CSharp
{
    internal sealed partial class LocalRewriter
    {
        public override BoundNode VisitIsnotOperator(BoundIsnotOperator node)
        {
            var boolean = GetSpecialType(SpecialType.System_Boolean, diagnostics, node);
            var boundIsOperator = new BoundIsOperator(node, node.Operand, isOperator.TargetType, isOperator.Conversion, isOperator.Type);

            return new BoundUnaryOperator(node, UnaryOperatorKind.BoolLogicalNegation, boundOperator,
                ConstantValue.NotAvailable, null, LookupResultKind.Viable, boolean)
                { WasCompilerGenerated = false };
        }

    }
}
