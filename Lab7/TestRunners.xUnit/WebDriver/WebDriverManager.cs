using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using OpenQA.Selenium.Firefox;

namespace TestRunners.xUnit.WebDriver;

public sealed class WebDriverManager
{
    private static readonly Lazy<WebDriverManager> Instance = new(() => new WebDriverManager());
    private readonly List<IWebDriver> _drivers = new();

    private WebDriverManager() { }

    public static WebDriverManager GetInstance() => Instance.Value;

    public IWebDriver CreateDriver()
    {
        // Thank you chrome dev team for tiling window manager support
        // reason for gecko switch: https://github.com/SeleniumHQ/selenium/issues/15358
        var options = new FirefoxOptions();
        options.AddArgument("--headless");
        var driver = new FirefoxDriver(options);
        driver.Manage().Window.Size = new Size(1920, 1080);
        _drivers.Add(driver);
        return driver;
    }

    public void QuitAllDrivers()
    {
        foreach (var driver in _drivers)
        {
            driver.Quit();
        }
        _drivers.Clear();
    }
} 