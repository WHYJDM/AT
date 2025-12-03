using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.xUnit;

public class TestBase : IDisposable
{
    private readonly List<IWebDriver> _drivers = [];

    protected WebDriverTools CreateAutomationTools()
    {
        var driver = new ChromeDriver();
        driver.Manage().Window.Maximize();
        driver.Manage().Window.Size = new Size(1920, 1080);
        
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var actions = new Actions(driver);
        
        _drivers.Add(driver);
        
        return new WebDriverTools
        {
            Driver = driver,
            Wait = wait,
            Actions = actions
        };
    }

    public void Dispose()
    {
        foreach (var driver in _drivers)
        {
            driver.Quit();
        }
        
        _drivers.Clear();
    }
}