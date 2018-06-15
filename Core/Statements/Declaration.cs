﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetJS.Core.Javascript {

    public enum DeclarationScope {
        Engine,
        Global,
        Function,
        Block
    }

    public class Declaration : Statement {
        public class DeclarationVariable {
            public Variable Variable;
            public Expression Expression;

            public DeclarationVariable(Variable variable, Expression expression) {
                Variable = variable;
                Expression = expression;
            }
        }

        public List<DeclarationVariable> Declarations = new List<DeclarationVariable>();

        public DeclarationScope Scope;
        public bool IsConstant;

        public Declaration(DeclarationScope scope, bool isConstant) {
            Scope = scope;
            IsConstant = isConstant;
        }

        public override Result Execute(Scope scope) {
            foreach (var declaration in Declarations) {
                Constant value = Static.Undefined;

                if (declaration.Expression != null) {
                    value = declaration.Expression.Execute(scope);
                }

                scope.DeclareVariable(declaration.Variable.Name, Scope, IsConstant, value, declaration.Variable.Type);
            }

            return new Result(ResultType.None);
        }
    }
}