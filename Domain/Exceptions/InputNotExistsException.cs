using System;

namespace Domain.Exceptions;

public sealed class InputNotExistsException : Exception
{
    public InputNotExistsException(): base("必須項目です。")
    {
    }

    public InputNotExistsException(string? message): base( message)
    {
    }

}
