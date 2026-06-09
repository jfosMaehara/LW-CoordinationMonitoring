using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.ConfigJson;
using Infrastructure.Logtext;
using Infrastructure.Sqlserver.RepositoriesImpl;
using Infrastructure.Access.RepositoriesImpl;

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
        SetStaticValuesConfigTest();
        var repo = new OrderImportEntityListRepositoryImpl();
        var list = repo.GetEntityList();
        if (OperatingSystem.IsWindows())
        {
            var mdbRepo = new CoordinationMDBCheckRepositoryImpl(list);
            var mdbList = mdbRepo.GetEntityList();
        }
    }
}