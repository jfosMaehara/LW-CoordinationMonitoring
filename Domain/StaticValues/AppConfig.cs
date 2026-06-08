using System;

namespace Domain.StaticValues;

public static class AppConfig
{
    public static string LogPath { get; set; } = string.Empty;
    public static bool IsTestMode { get; set; } = false;
    public static string ClientSymbol { get; set; } = "JF";
    public static string ApplicationName { get; set; } = string.Empty;
    public static string ApplicationTitle { get; set; } = string.Empty;
}