using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit;

public class TestBase
{
    private List<IWebDriver> _drivers = null!;
    
    [OneTimeSetUp]
    public void SetUp()
    {
        _drivers = new List<IWebDriver>();
    }
    
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

    [OneTimeTearDown]
    public void TearDown()
    {   
        foreach (var driver in _drivers)
        {
            driver.Quit();
        }
        
        _drivers.Clear();
    }
}