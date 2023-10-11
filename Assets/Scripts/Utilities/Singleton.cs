using UnityEngine;

//static instances are similar to singletons, but instead of destroying any new
//instances, it overrides the current instance. Handy for resetting the state
//and saves you doing it manually
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

//transforms the static instance into a basic singleton. This will destroy any
//new versions created, leaving the original instance intact
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        base.Awake();
    }
}

//A persistent version of the singleton. This will survive through scene loads.
//Perfect for system classes which require stateful, persistent data. Or audio
//sources where music plays through loading screens, etc.
public abstract class SingletonPersistent<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        base.Awake();
    }
}