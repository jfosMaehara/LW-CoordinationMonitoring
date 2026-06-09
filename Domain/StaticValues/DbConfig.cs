using System;

namespace Domain.StaticValues;

public static class DbConfig
{
    public static string SqlSvHost { get; set; } = string.Empty;
    public static string SqlSvName { get; set; } = string.Empty;
    public static string SqlSvUser { get; set; } = string.Empty;
    public static string SqlSvPass { get; set; } = string.Empty;
    public static string AccessProvider { get; set; } = string.Empty;
    public static string TestReplacementBefore { get; set; } = string.Empty;
    public static string TestReplacementAfter { get; set; } = string.Empty;
    public static string OfficeCD {  get; set; } = string.Empty;
    public static string BigCustomerCD {  get; set; } = string.Empty;
    public static string ShipperCD {  get; set; } = string.Empty;
    public static string CustomerDeptCD {  get; set; } = string.Empty;
    public static string EPCDB {  get; set; } = string.Empty;
}