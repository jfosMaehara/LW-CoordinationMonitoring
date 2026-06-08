namespace Domain.Repositories;

public interface ILoggerRepository
{
    /// <summary>
    /// ログの保存パス。
    /// </summary>
    public string LogPath {get;set;}

    /// <summary>
    /// ログの保存拡張子。
    /// </summary>
    public string Extension {get;set;}

    /// <summary>
    /// ログの保存期間(日数)。
    /// </summary>
    public int    LogHoldingDay {get;set;}

    /// <summary>
    /// レポジトリ ログを書き込む。
    /// </summary>
    /// <param name="level"></param>
    /// <param name="text"></param>
    public void LogWrite( LogLevel level, string text );

    /// <summary>
    /// レポジトリ 保存期間外のログを破棄する。
    /// </summary>
    public void LogTrash();

    /// <summary>
    /// ログ書き込み時のログレベル。
    /// </summary>
    public enum LogLevel
    {
        Start   = 0,
        End     = 1,
        Info    = 2,
        Warn    = 3,
        Fatal   = 4,
        Debug   = 9
    }
}