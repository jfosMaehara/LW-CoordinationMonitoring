namespace Domain.Helpers
{
    /// <summary>
    /// Directory クラスに関する汎用関数を管理するクラス
    /// </summary>
    public static class DirectoryUtils
    {
        /// <summary>
        /// 指定したパスにディレクトリが存在しない場合
        /// すべてのディレクトリとサブディレクトリを作成します
        /// </summary>
        /// <param name="path">検査するパス</param>
        /// <returns> 作成されたパスを文字列で返します。 すでに存在している場合は null が返却されます。 </returns>
        public static DirectoryInfo? SafeCreateDirectory( string path )
        {
            if ( Directory.Exists( path ) ) return null;
            return Directory.CreateDirectory( path );
        }
    }
}