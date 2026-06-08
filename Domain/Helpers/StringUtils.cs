using System.Text;

namespace Domain.Helpers;

public static class StringExtention
{
    public static string LeftB(this string s, Encoding encoding, int maxByteCount)
    {
        var bytes = encoding.GetBytes(s);
        if (bytes.Length <= maxByteCount) return s;
 
        var result = s[..encoding.GetString(bytes, 0, maxByteCount).Length];
 
        while (encoding.GetByteCount(result) > maxByteCount)
        {
            result = result[..^1];
        }
        return result;
    }     
}