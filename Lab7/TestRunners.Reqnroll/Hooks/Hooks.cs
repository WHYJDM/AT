using Reqnroll;
using TestRunners.Reqnroll.Steps;

namespace TestRunners.Reqnroll.Hooks;

[Binding]
public class Hooks
{
    private readonly WebsiteSteps _steps;

    public Hooks(WebsiteSteps steps)
    {
        _steps = steps;
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _steps.Dispose();
    }
} 