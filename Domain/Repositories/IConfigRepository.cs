namespace Domain.Repositories;

public interface IConfigRepository
{
    /// <summary>
    /// コンフィグファイルの中身取得 ※本来は中身の返却をするがコンフィグなので内部的にのみ処理する。
    /// </summary>
    public void GetConfig();

    /// <summary>
    /// GetConfig() にて取得した値を StaticValuesへ書き込む。
    /// </summary>
    public void SaveStaticValue();
}