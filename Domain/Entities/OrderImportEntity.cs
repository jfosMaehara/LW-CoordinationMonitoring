using Domain.Enums;
namespace Domain.Entities;

public class OrderImportEntity
(
    //受信取込管理番号
    string receivingCapturingManagementNumber,
    //枝番
    string branchNumber,
    //定義名
    string definitionName,
    //事業所CD
    string officeCd,
    //荷主CD
    string shipperCd,
    //荷主部門CD
    string customerDeptCd,
    //受注先CD
    string orderTargetCd,
    //連携TBL名
    string coordinationTBLName,
    //連携MDB_FULL_PATH
    string coordinationMDBFullPath
)
{
    /// <summary>
    /// 受信取込管理番号
    /// </summary>
    public string ReceivingCapturingManagementNumber => receivingCapturingManagementNumber;

    /// <summary>
    /// 枝番
    /// </summary>
    public string BranchNumber => branchNumber;

    /// <summary>
    /// 定義名
    /// </summary>
    public string DefinitionName => definitionName;

    /// <summary>
    /// 事業所CD
    /// </summary>
    public string OfficeCD => officeCd;

    /// <summary>
    /// 荷主CD
    /// </summary>
    public string ShipperCD => shipperCd;

    /// <summary>
    /// 荷主部門CD
    /// </summary>
    public string CustomerDeptCD => customerDeptCd;

    /// <summary>
    /// 受注先CD
    /// </summary>
    public string OrderTargetCD => orderTargetCd;

    /// <summary>
    /// 連携TBL名
    /// </summary>
    public string CoordinationTBLName => coordinationTBLName;

    /// <summary>
    /// 連携MDB_FULL_PATH
    /// </summary>
    public string CoordinationMDBFullPath => coordinationMDBFullPath;

    
    private CoordinationStatus _Status;
    /// <summary>
    /// ステータスコード（enum）
    /// </summary>
    public CoordinationStatus Status
    {
        get => _Status;
        set => _Status = value;
    }

    private string _StatusMessage;
    /// <summary>
    /// ステータスメッセージ
    /// </summary>
    public string StatusMessage
    {
        get => _StatusMessage;
        set => _StatusMessage = value;
    }

    private string _ExceptionMessage;
    /// <summary>
    /// Exceptionメッセージ
    /// </summary>
    public string ExceptionMessage
    {
        get => _ExceptionMessage;
        set => _ExceptionMessage = value;
    }
}
