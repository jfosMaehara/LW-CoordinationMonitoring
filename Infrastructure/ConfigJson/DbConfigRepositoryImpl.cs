using Domain.Repositories;
using Domain.Exceptions;
using Domain.StaticValues;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ConfigJson;

public class DbConfigRepositoryImpl() : IConfigRepository
{
    public string _SqlSvHost = string.Empty;
    public string _SqlSvName = string.Empty;
    public string _SqlSvUser = string.Empty;
    public string _SqlSvPass = string.Empty;

    public string _AccessProvider = string.Empty;

    public string _TestReplacementBefore = string.Empty;
    public string _TestReplacementAfter = string.Empty;

    public string _OfficeCD = string.Empty;
    public string _BigCustomerCD = string.Empty;
    public string _ShipperCd = string.Empty;
    public string _CustomerDeptCD = string.Empty;
    public string _EPCDB = string.Empty;

    public void GetConfig()
    {
        try
        {
            var json = new ConfigurationBuilder()
                        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                        .AddJsonFile(path: "DbSettings.json")
                        .Build();

            _AccessProvider = json["AccessProvider"] ?? string.Empty;

            if (AppConfig.OfficeCD == "10")
            {
                _SqlSvHost = json["SqlSvHost10"] ?? string.Empty;
                _SqlSvName = json["SqlSvName10"] ?? string.Empty;
                _SqlSvUser = json["SqlSvUser10"] ?? string.Empty;
                _SqlSvPass = json["SqlSvPass10"] ?? string.Empty;

                _TestReplacementBefore = json["TestReplacementBefore10"] ?? string.Empty;
                _TestReplacementAfter = json["TestReplacementAfeter10"] ?? string.Empty;

                _OfficeCD = json["OfficeCD10"] ?? string.Empty;
                _BigCustomerCD = json["BigCustomerCD10"] ?? string.Empty;
                _ShipperCd = json["ShipperCD10"] ?? string.Empty;
                _CustomerDeptCD = json["CustomerDeptCD10"] ?? string.Empty;
                _EPCDB = json["EPCDB10"] ?? string.Empty;
            }
            else
            {
                _SqlSvHost = json["SqlSvHost01"] ?? string.Empty;
                _SqlSvName = json["SqlSvName01"] ?? string.Empty;
                _SqlSvUser = json["SqlSvUser01"] ?? string.Empty;
                _SqlSvPass = json["SqlSvPass01"] ?? string.Empty;

                _TestReplacementBefore = json["TestReplacementBefore01"] ?? string.Empty;
                _TestReplacementAfter = json["TestReplacementAfeter01"] ?? string.Empty;

                _OfficeCD = json["OfficeCD01"] ?? string.Empty;
                _BigCustomerCD = json["BigCustomerCD01"] ?? string.Empty;
                _ShipperCd = json["ShipperCD01"] ?? string.Empty;
                _CustomerDeptCD = json["CustomerDeptCD01"] ?? string.Empty;
                _EPCDB = json["EPCDB01"] ?? string.Empty;
            }
        }
        catch
        {
            throw new ConfigFileException();
        }
    }

    public void SaveStaticValue()
    {
        DbConfig.SqlSvHost = _SqlSvHost;
        DbConfig.SqlSvName = _SqlSvName;
        DbConfig.SqlSvUser = _SqlSvUser;
        DbConfig.SqlSvPass = _SqlSvPass;
        DbConfig.AccessProvider = _AccessProvider;
        DbConfig.TestReplacementBefore = _TestReplacementBefore;
        DbConfig.TestReplacementAfter = _TestReplacementAfter;
        DbConfig.OfficeCD = _OfficeCD;
        DbConfig.BigCustomerCD = _BigCustomerCD;
        DbConfig.ShipperCD = _ShipperCd;
        DbConfig.CustomerDeptCD = _CustomerDeptCD;
        DbConfig.EPCDB = _EPCDB;
    }
}