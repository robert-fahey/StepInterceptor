using TechTalk.SpecFlow;

namespace StepInteceptor
{
    [Binding]
    public class StepInterceptionHook
    {
        private readonly StepInterceptorBuilder _builder;

        public StepInterceptionHook(StepInterceptorBuilder builder)
        {
            _builder = builder;
        }

        [BeforeStep()]
        public void BeforeStep()
        {
            Run(StepDefinitionHookType.BeforeStep, ScenarioStepContext.Current.StepInfo);
        }

        [AfterStep()]
        public void AfterStep()
        {
            Run(StepDefinitionHookType.AfterStep, ScenarioStepContext.Current.StepInfo);
        }

        private void Run(StepDefinitionHookType hookType, StepInfo stepInfo)
        {
            foreach (var executor in _builder.Build(hookType, stepInfo))
            {
                executor.Intercept();
            }
        }
    }
}