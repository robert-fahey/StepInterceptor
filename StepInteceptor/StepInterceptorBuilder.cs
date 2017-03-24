using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace StepInteceptor
{
    public class StepInterceptorBuilder
    {
        private readonly List<Func<StepInterceptor>> _interceptors;

        public StepInterceptorBuilder()
        {
            _interceptors = new List<Func<StepInterceptor>>();
        }

        public StepInterceptorBuilder BeforeWhen(Action action, int numberOfExecutions = 1)
        {
            return Register(StepDefinitionHookType.BeforeStep, StepDefinitionType.When, action, numberOfExecutions);
        }

        public StepInterceptorBuilder AfterWhen(Action action, int numberOfExecutions = 1)
        {
            return Register(StepDefinitionHookType.BeforeStep, StepDefinitionType.When, action, numberOfExecutions);
        }

        private StepInterceptorBuilder Register(StepDefinitionHookType stepDefinitionHookType,
            StepDefinitionType stepDefinitionType, Action action, int numberOfExecutions)
        {
            _interceptors.Add(
                () => new StepInterceptor(action, stepDefinitionHookType, stepDefinitionType, numberOfExecutions));
            return this;
        }


        public IEnumerable<StepInterceptor> Build()
        {
            return _interceptors.Select(factory => factory());
        }

        public IEnumerable<StepInterceptor> Build(StepDefinitionHookType hookType, StepInfo stepInfo)
        {
            return
                _interceptors.Select(factory => factory())
                    .Where(interceptor => interceptor.CanIntercept(hookType, stepInfo));
        }
    }
}