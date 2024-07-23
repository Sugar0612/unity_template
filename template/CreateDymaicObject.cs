/// <summary>
/// 动态创建类
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="name"></param>
/// <returns></returns>
public static T CreateObject<T>(string name) where T : class
{
    object obj = CreateObject(name);
    return obj == null ? null : obj as T;
}

/// <summary>
/// 动态创建类
/// </summary>
/// <param name="name"></param>
/// <returns></returns>
public static object CreateObject(string name)
{
    object obj = null;
    try
    {
        Type type = Type.GetType(name, true);
        obj = Activator.CreateInstance(type);
    }
    catch (Exception ex)
    {
        Debug.LogException(ex);
    }
    return obj;
}