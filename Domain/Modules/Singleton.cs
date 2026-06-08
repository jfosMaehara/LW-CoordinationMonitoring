using System;

namespace Domain.Modules
{
    /// <summary>
    /// Singletonパターン実装するための抽象クラス。
    /// コンストラクトでは常に Instance に ジェネリックでキャストされた自クラスが設定される。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        public static T? Instance;

        public Singleton()
        {
            Instance = (T)this;
        }
    }
}