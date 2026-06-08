using System;

namespace Domain.Exceptions;

public sealed class DataNotExistsException : Exception
{
    public DataNotExistsException(): base("対象データはありません。")
    {
    }

    public DataNotExistsException(string? message): base( message)
    {
    }

}
