using Domain.Repositories;
using Domain.Helpers;

namespace Infrastructure.Logtext;

public class LoggerRepositoryImpl( 
    string logPath,
    string extenstion = ".log",
    int    logHoldingDay = 14
) : ILoggerRepository
{
    public string LogPath {get;set;} = logPath;

    public string Extension {get;set;} = extenstion;

    public int    LogHoldingDay {get;set;} = logHoldingDay;
    
    private static string TodayString => DateTime.Today.ToString("yyyy-MM-dd");

    /// <summary>
    /// ログの書き込み
    /// </summary>
    /// <param name="level"></param>
    /// <param name="text"></param>
    public void LogWrite( ILoggerRepository.LogLevel level, string text  )
    {
        var file = LogPath + TodayString + Extension;
        DirectoryUtils.SafeCreateDirectory( LogPath );

        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var line = $@"{timestamp} : {level} -> {text} " + Environment.NewLine;
            
        // ロギングに失敗しても処理は続行させる。
        try
        {
            File.AppendAllText(file, line);
        }
        catch( Exception )
        {
            /* Windowsのイベントビューアへの書き込み用だが権限の関係でエラーになるので catch では行ってはいけない。
            var sourceName = "my.exe";
            if (EventLog.SourceExists(sourceName) == false) {
                EventLog.CreateEventSource(sourceName, "");
            }
            EventLog.WriteEntry(sourceName, "ログ:" + text , EventLogEntryType.Error, 64000);
            EventLog.WriteEntry(sourceName, "ロギングエラー:" + ex.ToString(), EventLogEntryType.Error, 64000);
            */
        }
    }

    /// <summary>
    /// 期間外ログの削除。
    /// </summary>
    public void LogTrash()
    {
        var files = Directory.GetFiles( LogPath, "*"+Extension );
        var today = DateTime.Today;
        
        foreach( var file in files )
        {
            var fileInfo = new FileInfo(file);
            var name = fileInfo.Name.Replace(  fileInfo.Extension, "");
            var sepalate =  name.Split("-");
            var year  = int.Parse( sepalate[0] );
            var month = int.Parse( sepalate[1] );
            var day   = int.Parse( sepalate[2] );
            var date  = new DateTime( year, month, day );
            var except = (today - date).Days;
            if( except > LogHoldingDay ) File.Delete( fileInfo.FullName );
        }
    }
}