﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Optimization;
using Optimization.Interfaces;
using Optimization.Solver.GLPK;
using OtimizacaoFoodTruck.Entitys;

namespace OtimizacaoFoodTruck.Business
{
    public class GlpkBusiness
    {
        private readonly GLPKSolver GLPK = new GLPKSolver();
        public bool Otimizacao(Ingredientes ingredientes)
        {
            Model model = new Model();
            List<Variable> list = AdicionarVariaveis(ingredientes);
            model.AddVariables(list);
            model.AddConstraint(Constraint.GreaterThanOrEqual(MontarExpressaoCustoXQuantidade(ingredientes),ingredientes.CapitalDeGiro ));
            model = AdicionarObjetivo(list, model);
            return GLPK.Solve(model) == null;
        }
        private Expression MontarExpressaoCustoXQuantidade(Ingredientes ingredientes)
        {
            Variable variable = new Variable
            {
                Value = ingredientes.QuantidadeTotalDeIngredientesAComprar * (ingredientes.PrecoBacon + ingredientes.PrecoBatataPalha + ingredientes.PrecoKetchup + ingredientes.PrecoMilho + ingredientes.PrecoPao + ingredientes.PrecoSalsicha),
                Name = "CustoTotal",
                Type = VariableType.Continuous,

            };
            return Expression.Sum(new List<Variable> { variable });

        }
        private Model AdicionarObjetivo(List<Variable> list, Model model)
        {
            List<Variable> lista = new List<Variable>();
            Objective objective = new Objective(Expression.Sum(list));
            model.AddObjective(objective);
            return model;
        }
        private List<Variable> AdicionarVariaveis(Ingredientes ingredientes)
        {
            Variable QuantidadeBacon = new Variable
            {
                Type = VariableType.Integer,
                Name = "QuantidadeBacon",
                LowerBound = 1,
                UpperBound = 10000000,
                Value = ingredientes.QuantidadeBacon
            };
            Variable QuantidadeMilho = new Variable
            {
                Type = VariableType.Integer,
                Name = "QuantidadeMilho",
                LowerBound = 1,
                UpperBound = 10000000,
                Value = ingredientes.QuantidadeMilho
            };
            Variable PrecoBacon = new Variable
            {
                Type = VariableType.Continuous,
                Name = "PrecoBacon",
                LowerBound = 1,
                UpperBound = 10000000,
                Value = ingredientes.PrecoBacon
            };
            Variable PrecoMilho = new Variable
            {
                Type = VariableType.Continuous,
                Name = "PrecoMilho",
                LowerBound = 1,
                UpperBound = 10000000,
                Value = ingredientes.PrecoMilho
            };
            Variable CapitalGiro = new Variable
            {
                Type = VariableType.Continuous,
                Name = "Capital de giro",
                LowerBound = 1,
                UpperBound = 1000000000000000,
                Value = ingredientes.CapitalDeGiro,
            };
            List<Variable> lista = new List<Variable>
            {
                CapitalGiro,
                PrecoBacon,
                PrecoMilho,
                QuantidadeMilho,
                QuantidadeBacon
            };
            return lista;
        }
    }
}