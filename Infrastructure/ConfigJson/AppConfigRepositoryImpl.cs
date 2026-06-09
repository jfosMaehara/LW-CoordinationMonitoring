using Domain.Repositories;
using Domain.Exceptions;
using Domain.StaticValues;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ConfigJson;

public class AppConfigRepositoryImpl() : IConfigRepository
{
    public string _logPath    = AppDomain.CurrentDomain.BaseDirectory + @"\log\";
    public bool   _isTestMode = false;
    public string _clientSymbol = string.Empty;
    public string _applicationName = string.Empty;
    public string _applicationTitle = string.Empty;
    public string _officeCd = string.Empty;
    public int _interval = 300000;
    
    public void GetConfig()
    {
        try
        {
            var json = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(path: "AppSettings.json")
                        .Build();

            if (string.IsNullOrEmpty(json["LogPath"] ?? string.Empty))
            {
                _logPath = AppDomain.CurrentDomain.BaseDirectory + @"\log\";
            }
            else
            {
                _logPath = json["LogPath"] ?? string.Empty;
            }

            _isTestMode = bool.Parse(json["IsTestMode"] ?? "False");
            _clientSymbol = json["ClientSymbol"] ?? "JF";
            _applicationName = json["ApplicationName"] ?? string.Empty;
            _applicationTitle = json["ApplicationTitle"] ?? string.Empty;
            _officeCd = json["OfficeCD"] ?? string.Empty;
            _interval = int.Parse(json["ConfirmationIntervalMillisecond"] ?? "300000");
        }
        catch
        {
            throw new ConfigFileException();
        }
    }

    public void SaveStaticValue()
    {
        AppConfig.LogPath = _logPath;
        AppConfig.IsTestMode = _isTestMode;
        AppConfig.ClientSymbol = _clientSymbol;
        AppConfig.ApplicationName = _applicationName;
        AppConfig.ApplicationTitle = _applicationTitle;
        AppConfig.OfficeCD = _officeCd;
        AppConfig.ConfirmationIntervalMillisecond = _interval;
    }
}