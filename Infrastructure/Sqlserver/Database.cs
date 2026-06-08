using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Domain.Modules;
using System.Transactions;

namespace Infrastructure.Sqlserver;

/// <summary>
/// Singletonパターンを継承したDatabaseの抽象クラス
/// </summary>
public abstract class Database : IDisposable
{
    public int CommandTimeoutSpan {get; set;} = 300;

    public SqlConnection Connection {get; set;}

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public Database()
    {
        Connection = new ();
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~Database()
    {
        Dispose();
    }

    /// <summary>
    /// ガーベッジ処理
    /// </summary>
    public void Dispose()
    {
        Connection?.Close();
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// SELECT句を実行してDataTableへ格納する。
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataTable SelectInDataTable(string sql)
    {
        using var command = new SqlCommand( sql, Connection );
        using var adapter = new SqlDataAdapter( command );
        command.CommandTimeout = CommandTimeoutSpan;
        var dt = new DataTable();
        adapter.Fill( dt );
        return dt;
    }

    /// <summary>
    /// SELECT句を実行して1行(先頭行)のDataRowを返却する。
    /// 基本的にはEntityが確保されたSQLを流し込んでユニークな1行を取得することに使う。
    /// SELECT句がヒットしなければ null が返却される。
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public DataRow? SelectOneDataRow(string sql)
    {
        using var command = new SqlCommand( sql, Connection );
        using var adapter = new SqlDataAdapter( command );
        command.CommandTimeout = CommandTimeoutSpan;
        var dt = new DataTable();
        adapter.Fill( dt );
        if( dt.Rows.Count == 0 ) return null;
        return dt.Rows[0];
    }

    /// <summary>
    /// テーブルが返らないSQLを実行する。非トランザクション。 
    /// Connectionが Closed の場合はOpenしてからQueueを実行する。
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int? Execute(string sql )
    {
        using var command = new SqlCommand( sql, Connection );
        command.CommandTimeout = CommandTimeoutSpan;
        if ( Connection.State == ConnectionState.Closed ) Connection.Open();
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// オーバーロード 
    /// テーブルが返らないSQLを実行する。トランザクションを含める。
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public int? Execute(string sql, SqlTransaction transaction)
    {
        using var command = new SqlCommand( sql, Connection, transaction );
        command.CommandTimeout = CommandTimeoutSpan;
        if ( Connection.State == ConnectionState.Closed ) Connection.Open();
        return command.ExecuteNonQuery();
    }
    
    /// <summary>
    /// コネクションが閉じていた場合はオープンし、オープンされている状態だった場合は再オープンはしない。(エラー対策)
    /// </summary>
    /// <returns></returns>
    public ConnectionState Open()
    {
        if( Connection.State ==  ConnectionState.Closed ) Connection.Open();
        return Connection.State;
    }

    /// <summary>
    /// コネクションがオープンしている状態なら閉じる。(エラー対策)
    /// </summary>
    /// <returns></returns>
    public ConnectionState Close()
    {
        if( Connection.State ==  ConnectionState.Open ) Connection.Close();
        return Connection.State;
    }

    /// <summary>
    /// コネクションが閉じていたら開いてトランザクションを開始する。
    /// </summary>
    /// <returns></returns>
    public SqlTransaction BeginTransaction()
    {
        if( Connection.State ==  ConnectionState.Closed ) Connection.Open();
        return Connection.BeginTransaction();
    }

}