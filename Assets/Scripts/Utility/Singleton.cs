using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (FindObjectsByType<T>(FindObjectsSortMode.None).Length > 1)
                {
                    Debug.LogWarning("More than one instance of " + typeof(T) + " found!");
                    return instance;
                }

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
