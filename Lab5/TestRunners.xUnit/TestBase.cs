using TestRunners.xUnit.Configuration;
using TestRunners.xUnit.WebDriver;

namespace TestRunners.xUnit;

public class TestBase
{
    private readonly WebDriverManager _driverManager;

    protected TestBase()
    {
        _driverManager = WebDriverManager.GetInstance();
    }

    protected TestConfiguration CreateTestConfiguration()
    {
        var driver = _driverManager.CreateDriver();
        return new TestConfiguration.Builder()
            .WithDriver(driver)
            .WithTimeout(TimeSpan.FromSeconds(10))
            .Build();
    }
    
    public void Dispose()
    {
        _driverManager.QuitAllDrivers();
        Environment.Exit(0); // TODO: On linux browsers don't exit, leaving orphan processes, fix
    }
}