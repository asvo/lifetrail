/// <summary>
/// 单例基类
/// </summary>
public class ISingleton<T> where T : new () {

    public static T Instance
    {
        get
        {
            if (null == mIstance)
            {
                mIstance = new T();
            }
            return mIstance;
        }
    }

    private static T mIstance;
}
