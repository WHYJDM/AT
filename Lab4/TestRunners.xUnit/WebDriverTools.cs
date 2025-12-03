using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.xUnit;

public class WebDriverTools
{
    public required IWebDriver Driver { get; init; }
    public required WebDriverWait Wait { get; init; }
    public required Actions Actions { get; init; }
}