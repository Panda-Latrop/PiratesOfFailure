using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance;

    internal static T Instance {
        get {
            if (instance == null) {
                Debug.LogWarning(string.Format("^Singleton call before awake!!! Type of {0}^", typeof (T)));

                instance = FindObjectOfType(typeof (T)) as T;
                if (instance != null) {
                    DontDestroyOnLoad(instance.gameObject);
                }
            }

            if (instance == null) {
                var obj = new GameObject(typeof (T).ToString());
                instance = obj.AddComponent(typeof (T)) as T;

                if (instance != null) {
                    DontDestroyOnLoad(instance.gameObject);
                }
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(instance.gameObject);
        } else if (instance != gameObject.GetComponent<T>()) {
            Destroy(gameObject);
        }
    }
    
}
