using System;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Runtime.Versioning;

namespace Infrastructure.Access;

/// <summary>
/// AccessDBクラス
/// </summary>
[SupportedOSPlatform("windows")]
public class AccessDatabase : IDisposable
{
    private string _connectionString = string.Empty;
    private string _dataSource = string.Empty;
    private string _provider = string.Empty;

    public OleDbConnection Connection { get; set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="provider"></param>
    public AccessDatabase(string dataSource, string provider)
    {
        _dataSource = dataSource;
        _provider = provider;
        _connectionString = ConnectionStringBuilder(_dataSource, _provider);

        Connection = new OleDbConnection(_connectionString);
    }

    /// <summary>
    /// デストラクタ
    /// </summary>
    ~AccessDatabase()
    {
        Dispose();
    }

    /// <summary>
    /// ガベージ処理
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
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        using var command = new OleDbCommand(sql, Connection);
        var dt = new DataTable();
        using var adapter = new OleDbDataAdapter(command);
        adapter.Fill(dt);
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
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        using var command = new OleDbCommand(sql, Connection);
        var dt = new DataTable();
        using var adapter = new OleDbDataAdapter(command);
        adapter.Fill(dt);
        if (dt.Rows.Count == 0) return null;
        return dt.Rows[0];
    }

    /// <summary>
    /// テーブルが返らないSQLを実行する。非トランザクション。 
    /// Connectionが Closed の場合はOpenしてからQueueを実行する。
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public int? Execute(string sql)
    {
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        using var command = new OleDbCommand(sql, Connection);
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// オーバーロード 
    /// テーブルが返らないSQLを実行する。トランザクションを含める。
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public int? Execute(string sql, OleDbTransaction transaction)
    {
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        using var command = new OleDbCommand(sql, Connection);
        command.Transaction = transaction;
        return command.ExecuteNonQuery();
    }

    /// <summary>
    /// コネクションが閉じていた場合はオープンし、オープンされている状態だった場合は再オープンはしない。(エラー対策)
    /// </summary>
    public ConnectionState Open()
    {
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        return Connection.State;
    }

    /// <summary>
    /// コネクションがオープンしている状態なら閉じる。(エラー対策)
    /// </summary>
    public ConnectionState Close()
    {
        if (Connection.State == ConnectionState.Open) Connection.Close();
        return Connection.State;
    }

    /// <summary>
    /// コネクションが閉じていたら開いてトランザクションを開始する。
    /// </summary>
    /// <returns></returns>
    public OleDbTransaction BeginTransaction()
    {
        if (Connection.State == ConnectionState.Closed) Connection.Open();
        return Connection.BeginTransaction();
    }

    /// <summary>
    /// ConnectionString作成
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    private string ConnectionStringBuilder(string dataSource, string provider)
    {
        return $@"Provider={provider};Data Source={dataSource}";
    }

    //public AccessDatabase()
    //{
    //    var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=./Test.mdb";
    //    var query = "SELECT * FROM TestTable";
    //    try
    //    {
    //        using (OleDbConnection connection = new OleDbConnection(connectionString))
    //        {
    //            connection.Open();
    //            using (OleDbCommand command = new OleDbCommand(query, connection))
    //            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
    //            {
    //                DataTable table = new DataTable();

    //                // 3. DataTable に結果を取得（Fill でSELECT結果が入る） [InlineCitation-1-【C#】Accessに接続してデータを取得する方法（SQLインジェクション対策も） #初心者 - Qiita](https://qiita.com/okayu__11/items/cfa9938f6c9ed6b9db48) [InlineCitation-2-【C#】C#からAccessのテーブルデータを取得するには | tech-vb](https://tech-vb.com/c_sharp_retrieve_access_table_data_from_c_sharp)
    //                adapter.Fill(table);

    //                // 4. 結果表示
    //                foreach (DataRow row in table.Rows)
    //                {
    //                    foreach (DataColumn col in table.Columns)
    //                    {
    //                        Debug.Write($"{col.ColumnName}: {row[col]}  ");
    //                    }
    //                    Debug.WriteLine("");
    //                }
    //            }

    //        }
    //    }
    //    catch
    //    {

    //    }
    //}
}
