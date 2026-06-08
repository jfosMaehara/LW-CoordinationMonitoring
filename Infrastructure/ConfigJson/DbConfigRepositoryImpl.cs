using Domain.Repositories;
using Domain.Exceptions;
using Domain.StaticValues;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ConfigJson;

public class DbConfigRepositoryImpl() : IConfigRepository
{
    public string _DbHost = string.Empty;
    public string _DbUser = string.Empty;
    public string _DbPass = string.Empty;
    public string _DbName = string.Empty;
    public void GetConfig()
    {
        try
        {
            var json = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(path: "DbSettings.json")
                        .Build();

            _DbHost = json["DbHost"] ?? string.Empty;
            _DbUser = json["DbUser"] ?? string.Empty;
            _DbPass = json["DbPass"] ?? string.Empty;
            _DbName = json["DbName"] ?? string.Empty;

        }
        catch
        {
            throw new ConfigFileException();
        }
    }

    public void SaveStaticValue()
    {
        DbConfig.DbHost = _DbHost;
        DbConfig.DbUser = _DbUser;
        DbConfig.DbPass = _DbPass;
        DbConfig.DbName = _DbName;
    }
}