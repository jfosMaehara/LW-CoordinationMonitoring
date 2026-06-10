namespace Domain.Enums;

public enum CoordinationStatus
{
    /// <summary>
    /// 未連携
    /// </summary>
    NotCoordinated = 0,
    /// <summary>
    /// 連携データなし
    /// </summary>
    NoData = -1,
    /// <summary>
    /// 連携データテーブルなし
    /// </summary>
    NoTable = -2,
}
