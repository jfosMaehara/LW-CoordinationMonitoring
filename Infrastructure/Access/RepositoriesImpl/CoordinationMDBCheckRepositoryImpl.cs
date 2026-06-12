using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.Logtext;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

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
        try
        {
            for (var i = 0; i < checkList.Count; i++)
            {
                var mdbPath = checkList[0].CoordinationMDBFullPath;
                using var db = new AccessDatabase(mdbPath, DbConfig.AccessProvider);
                try
                {
                    var dt = db.SelectInDataTable(GetQuery(checkList[i].CoordinationTBLName));
                    var status = dt.Rows.Count > 0 ? CoordinationStatus.NotCoordinated : CoordinationStatus.NoData;
                    checkList[i].Status = status;
                    checkList[i].StatusMessage = status == CoordinationStatus.NotCoordinated ? "未連携" : "連携データなし";
                }
                catch (OleDbException e)
                {
                    checkList[i].Status = CoordinationStatus.NoTable;
                    checkList[i].StatusMessage = "連携データテーブルなし";
                    checkList[i].ExceptionMessage = e.Message;
                    db.Close();
                    db.Dispose();
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
