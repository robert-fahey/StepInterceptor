using TechTalk.SpecFlow;

namespace Specflow.StepInterceptor
{
    [Binding]
    public class StepInterceptionHook
    {
        private readonly StepInterceptorBuilder _builder;
        private readonly ScenarioContext _scenarioContext;

        public StepInterceptionHook(StepInterceptorBuilder builder, ScenarioContext scenarioContext)
        {
            _builder = builder;
            _scenarioContext = scenarioContext;
        }

        [BeforeStep()]
        public void BeforeStep()
        {
            Run(StepDefinitionHookType.BeforeStep, _scenarioContext.StepContext.StepInfo);
        }

        [AfterStep()]
        public void AfterStep()
        {
            Run(StepDefinitionHookType.AfterStep, _scenarioContext.StepContext.StepInfo);
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