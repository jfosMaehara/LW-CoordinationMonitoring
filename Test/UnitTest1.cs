using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;

namespace Test;

public class UnitTest1
{
    public static ILoggerRepository? Logger { get; set; }
    
    [Fact]
    public static void SetStaticValuesConfigTest()
    {
        var config = new AppConfigRepositoryImpl();
        config.GetConfig();
        config.SaveStaticValue();

        var dbConfig = new DbConfigRepositoryImpl();
        dbConfig.GetConfig();
        dbConfig.SaveStaticValue();

        Logger = new LoggerRepositoryImpl(config._logPath);
    }

    [Fact]
    public void Test1()
    {
    }
}