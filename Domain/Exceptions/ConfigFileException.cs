namespace Domain.Exceptions;

public class ConfigFileException : Exception
{
    public ConfigFileException() : base("コンフィグファイルの読み込みに失敗しました。")
    {
    }
}