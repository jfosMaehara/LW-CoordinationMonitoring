using System;

namespace Domain.StaticValues;

public static class DbConfig
{
    public static string DbHost { get; set; } = string.Empty;
    public static string DbUser { get; set; } = string.Empty;
    public static string DbPass { get; set; } = string.Empty;
    public static string DbName { get; set; } = string.Empty;
    
}