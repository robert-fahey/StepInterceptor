using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace StepInterceptor
{
    public class StepInterceptor
    {
        private readonly Action _action;
        private readonly StepDefinitionType _definitionType;
        private readonly StepDefinitionHookType _hookType;
        private readonly int _numberOfExecutions;

        private int _executionCount;

        public StepInterceptor(Action action, StepDefinitionHookType hookType, StepDefinitionType stepDefinitionType, int numberOfExecutions)
        {
            _action = action;
            _definitionType = stepDefinitionType;
            _numberOfExecutions = numberOfExecutions;
            _hookType = hookType;
        }

        public bool CanIntercept(StepDefinitionHookType hookType, StepInfo stepInfo)
        {
            return _hookType == hookType && _definitionType == stepInfo.StepDefinitionType;
        }

        public void Intercept()
        {
            if (_executionCount >= _numberOfExecutions) return;
            _action();
            _executionCount++;
        }
    }
}