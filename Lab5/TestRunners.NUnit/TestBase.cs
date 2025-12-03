using NUnit.Framework;
using TestRunners.NUnit.Configuration;
using TestRunners.NUnit.WebDriver;

namespace TestRunners.NUnit;

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
    
    [OneTimeTearDown]
    public void TearDown()
    {
        _driverManager.QuitAllDrivers();
        Environment.Exit(0);
    }
}