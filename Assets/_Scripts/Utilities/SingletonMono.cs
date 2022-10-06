using UnityEngine;

public class SingletonMono<T> : MonoBehaviour, IInitializable 
    where T : MonoBehaviour
{
    public static T Instance;
    
    public void Initialize()
    {
        if (Instance == null)
            InitSingleInstance();
        else
            Destroy(gameObject);
    }

    protected virtual void InitSingleInstance()
    {
        Instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}