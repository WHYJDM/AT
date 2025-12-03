using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TestRunners.NUnit.Configuration;

public class TestConfiguration
{
    public IWebDriver Driver { get; private set; }
    public WebDriverWait Wait { get; private set; }
    public Actions Actions { get; private set; }
    public TimeSpan Timeout { get; private set; }

    private TestConfiguration() { }

    public class Builder
    {
        private readonly TestConfiguration _configuration;

        public Builder()
        {
            _configuration = new TestConfiguration
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public Builder WithDriver(IWebDriver driver)
        {
            _configuration.Driver = driver;
            return this;
        }

        public Builder WithTimeout(TimeSpan timeout)
        {
            _configuration.Timeout = timeout;
            return this;
        }

        public TestConfiguration Build()
        {
            _configuration.Wait = new WebDriverWait(_configuration.Driver, _configuration.Timeout);
            _configuration.Actions = new Actions(_configuration.Driver);
            return _configuration;
        }
    }
} 