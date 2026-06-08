using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using Microsoft.VisualBasic;

namespace Domain.Helpers
{
    public static class KanaConv
    {
        [SupportedOSPlatform("windows")]
        public static string GetKana(string text)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var type = Type.GetTypeFromProgID("MSIME.Japan");
            if( type == null ) return string.Empty;

            try 
            {
                if (Activator.CreateInstance(type) is not IFELanguage ife) return "";
                ife.Open();
                ife.GetPhonetic(text, 1, -1, out string hiragana);
                return Strings.StrConv( hiragana, VbStrConv.Katakana, 0x0411 ) ?? string.Empty;
            } 
            catch
            {
                return string.Empty;
            }

        }

        [SupportedOSPlatform("windows")]
        public static string GetHanKana(string text)
        {
            try
            {
                var kana = GetKana(text) ?? string.Empty;
                return Strings.StrConv( kana, VbStrConv.Narrow,  0x0411 ) ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        [ComImport]
        [Guid("019F7152-E6DB-11D0-83C3-00C04FDDB82E")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IFELanguage
        {
            int Open();
            int Close();

            void Dummy1();  // not use
            void Dummy2();  // not use

            int GetPhonetic(
                [MarshalAs(UnmanagedType.BStr)] string src,
                int start,
                int length,
                [MarshalAs(UnmanagedType.BStr)] out string dest);
        }
    }
}