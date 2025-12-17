using TestRunners.xUnit.Configuration;
using TestRunners.xUnit.WebDriver;
using Serilog;
using Serilog.Events;
using System.Reflection;

namespace TestRunners.xUnit;

public class TestBase : IDisposable
{
    private readonly WebDriverManager _driverManager;
    protected ILogger Logger { get; private set; }

    protected TestBase()
    {
        _driverManager = WebDriverManager.GetInstance();
        SetupLogging();
    }

    private void SetupLogging()
    {
        var logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "logs", "test.log");
        Directory.CreateDirectory(Path.GetDirectoryName(logPath) ?? string.Empty);

        Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(logPath, 
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        Logger.Information("Test session started");
    }

    protected TestConfiguration CreateTestConfiguration()
    {
        var driver = _driverManager.CreateDriver();
        Logger.Debug("Created new WebDriver instance");
        return new TestConfiguration.Builder()
            .WithDriver(driver)
            .WithTimeout(TimeSpan.FromSeconds(10))
            .Build();
    }
    
    public void Dispose()
    {
        Logger.Information("Test session completed");
        _driverManager.QuitAllDrivers();
        Environment.Exit(0); // TODO: On linux browsers don't exit, leaving orphan processes, fix
    }
}