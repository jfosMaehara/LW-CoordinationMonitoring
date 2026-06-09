using Domain.Entities;
using Domain.Repositories;
using Domain.StaticValues;
using Infrastructure.Logtext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Sqlserver.RepositoriesImpl;

/// <summary>
/// 未連携一覧取得
/// </summary>
/// <param name="isTest">true:テスト</param>
/// <param name="replacementBefore">テスト時の置換対象文字列</param>
/// <param name="replacementAfter">テスト時の置き換え文字</param>
public class OrderImportEntityListRepositoryImpl() : IEntityListRepository<OrderImportEntity>
{
    private LoggerRepositoryImpl _logger = new LoggerRepositoryImpl(AppConfig.LogPath);
    public List<OrderImportEntity> GetEntityList()
    {
        using var db = new SqlSvDatabase(DbConfig.SqlSvHost, DbConfig.SqlSvName, DbConfig.SqlSvUser, DbConfig.SqlSvPass);
        _logger.LogWrite(ILoggerRepository.LogLevel.Info, db.Connection.ConnectionString);
        var query = GetQuery();
        _logger.LogWrite(ILoggerRepository.LogLevel.Info, query.ToString());
        using var dt = db.SelectInDataTable(query);
        var list = new List<OrderImportEntity>();
        foreach (DataRow row in dt.Rows) list.Add(Mapping(row));
        return list;
    }

    private OrderImportEntity Mapping(DataRow row)
    {
        var fullPath = row["連携MDB_FULL_PATH"] != DBNull.Value ? (string)row["連携MDB_FULL_PATH"] : string.Empty;
        var fullRepPath = GetReplacement(AppConfig.IsTestMode, DbConfig.TestReplacementBefore, DbConfig.TestReplacementAfter, fullPath);
        return new(
            row["受信取込管理番号"] != DBNull.Value ? (string)row["受信取込管理番号"] : string.Empty,
            row["枝番"] != DBNull.Value ? (string)row["枝番"] : string.Empty,
            row["定義名"] != DBNull.Value ? (string)row["定義名"] : string.Empty,
            row["事業所CD"] != DBNull.Value ? (string)row["事業所CD"] : string.Empty,


            row["荷主CD"] != DBNull.Value ? (string)row["荷主CD"] : string.Empty,
            row["荷主部門CD"] != DBNull.Value ? (string)row["荷主部門CD"] : string.Empty,
            row["受注先CD"] != DBNull.Value ? (string)row["受注先CD"] : string.Empty,
            row["連携TBL名"] != DBNull.Value ? (string)row["連携TBL名"] : string.Empty,
            fullRepPath
            );
    }

    private string GetQuery()
    {
        return $@"
            SELECT
                受信取込管理番号
            ,    枝番
            ,    定義名
            ,    事業所CD
            ,    荷主CD
            ,    荷主部門CD
            ,    受注先CD
            ,    連携TBL名
            ,    連携MDB_FULL_PATH = 連携FILEPATH + 連携FILE名 + 連携FILE拡張子
            
            FROM
                {DbConfig.SqlSvName}.dbo.LM受信取込定義 AS P
            WHERE
                EXISTS 
                (
                    SELECT
                        *
                    FROM
                        {DbConfig.EPCDB}.dbo.EJ受注出荷売上 AS C
                    WHERE
                        事業所CD = '{DbConfig.OfficeCD}'
                    AND    PIC中状況FLG = '1'
                    AND 出荷待状況FLG= '1'
                    AND 売上区分 <> '9'
                    AND 出荷確定状況FLG = '0'
                    AND (
                                直送区分 NOT IN ('1', '3') 
                            OR    (
                                        直送区分='2' 
                                    AND    振替出荷事業所CD IS NULL
                                )
                        )
                    AND NOT EXISTS 
                        (
                            SELECT
                                *
                            FROM
                                {DbConfig.SqlSvName}.dbo.LJ受注連携累積V2 AS G
                            WHERE
                                G.管理番号 = C.管理番号
                            AND G.管理行番号 = C.管理行番号
                        )
                    AND P.大荷主CD = '{DbConfig.BigCustomerCD}'
                    AND P.荷主CD = '{DbConfig.ShipperCD}'
                    AND P.荷主部門CD = '{DbConfig.CustomerDeptCD}'
                    AND P.事業所CD = C.事業所CD
                    AND P.受注先CD = C.お客様CD
                    AND RIGHT(P.連携TBL名, 4 ) = C.請求先CD --請求先とテーブル名が違って登録されている可能性がある。 その場合この条件は排除する必要があるが今のところはないはず。
                )
            
        ";
    }
    
    private string GetReplacement(bool isTest, string before, string after, string fullPath)
    {
        var res = fullPath;
        if (isTest)
        {
            res = fullPath.Replace(before, after);
        }
        return res;
    }
}
