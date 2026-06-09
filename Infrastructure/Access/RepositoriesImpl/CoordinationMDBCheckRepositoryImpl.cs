using Domain.Entities;
using Domain.Repositories;
using Domain.StaticValues;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Infrastructure.Access.RepositoriesImpl;

[SupportedOSPlatform("windows")]
public class CoordinationMDBCheckRepositoryImpl
(
    List<OrderImportEntity> checkList
) : IEntityListRepository<OrderImportEntity>
{
    public List<OrderImportEntity> GetEntityList()
    {
        var list = new List<OrderImportEntity>();
        if (checkList == null || checkList.Count == 0) return list;
        var mdbPath = checkList[0].CoordinationMDBFullPath;
        using var db = new AccessDatabase(mdbPath, DbConfig.AccessProvider);
        try
        {
            for (var i = 0; i < checkList.Count; i++)
            {
                try
                {
                    var dt = db.SelectInDataTable(GetQuery(checkList[i].CoordinationTBLName));
                    var status = dt.Rows.Count > 0 ? CoordinationStatus.NotCoordinated : CoordinationStatus.NoData;
                    checkList[i].Status = status;
                }
                catch (OleDbException e)
                {
                    checkList[i].Status = CoordinationStatus.NoData;
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return checkList;
    }

    private string GetQuery(string tblName)
    {
        return $@"SELECT TOP 1 * FROM {tblName}";
    }
}
