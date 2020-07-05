using UnityEngine;

/// <summary>
/// Declares this script as a singleton that can be referenced statically with T.Instance
/// </summary>
/// <typeparam name="T"></typeparam> hello
public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : Object
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
            }
            return _instance;
        }
    }
}
