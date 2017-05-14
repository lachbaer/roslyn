// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;
using Microsoft.CodeAnalysis.CSharp;

namespace Microsoft.CodeAnalysis.CSharp
{
    internal sealed partial class LocalRewriter
    {
        public override BoundNode VisitIsnotOperator(BoundIsnotOperator node)
        {
            var boundIsOperator = new BoundIsOperator(node.Syntax, node.Operand, node.TargetType, node.Conversion, node.Type);

            return new BoundUnaryOperator(node.Syntax, UnaryOperatorKind.BoolLogicalNegation, boundIsOperator,
                ConstantValue.NotAvailable, null, LookupResultKind.Viable,
                type: _compilation.GetSpecialType(SpecialType.System_Boolean))
                { WasCompilerGenerated = false };
        }

        public override BoundNode VisitIsnotPatternExpression(BoundIsnotPatternExpression node)
        {
            var boundIsPatternExpression = new BoundIsPatternExpression(node.Syntax, node.Expression, node.Pattern, node.Type);

            return new BoundUnaryOperator(node.Syntax, UnaryOperatorKind.BoolLogicalNegation, boundIsPatternExpression,
                ConstantValue.NotAvailable, null, LookupResultKind.Viable,
                type: _compilation.GetSpecialType(SpecialType.System_Boolean))
            { WasCompilerGenerated = false };

            //*EIK this was the previous implementation
            var loweredExpression = VisitExpression(node.Expression);
            var loweredPattern = LowerPattern(node.Pattern);
            Debug.Assert(loweredPattern.Kind == BoundKind.ConstantPattern, "Isnot with declaration or wildcard cannot be here!");

            var declPattern = (BoundDeclarationPattern)loweredPattern;
            return MakeIsDeclarationPattern(declPattern, loweredExpression);
        }
    }
}
