using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Serilog;

namespace TestRunners.Reqnroll.Steps;

public class TestBase
{
    protected IWebDriver Driver { get; private set; }
    protected WebDriverWait Wait { get; private set; }
    protected ILogger Logger { get; private set; }

    protected TestBase()
    {
        Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        var options = new FirefoxOptions
        {
            PageLoadTimeout = TimeSpan.FromSeconds(60),
            ScriptTimeout = TimeSpan.FromSeconds(60),
            ImplicitWaitTimeout = TimeSpan.FromSeconds(60),
        };
        Driver = new FirefoxDriver(options);
        Driver.Manage().Window.Size = new Size(1920, 1080);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
    }

    public void Dispose()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }
} 