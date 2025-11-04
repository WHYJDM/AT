using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace WebUIAutomation;

public class TestBase
{
    protected IWebDriver Driver = null!;
    protected WebDriverWait Wait = null!;
    protected Actions Actions = null!;

    [SetUp]
    public void SetUp()
    {
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
        Driver.Manage().Window.Size = new Size(1920, 1080);
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        Actions = new Actions(Driver);
    }

    [TearDown]
    public void TearDown()
    {
        Driver.Quit();
    }
}