using UnityEngine;

public class DestroyableSingleton<T> : MonoBehaviour where T : DestroyableSingleton<T> {
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                Debug.LogWarning(string.Format("^Singleton call before awake!!! Type of {0}^", typeof(T)));

                instance = FindObjectOfType(typeof(T)) as T;
            }

            if (instance == null) {
                var obj = new GameObject(typeof(T).ToString());
                instance = obj.AddComponent(typeof(T)) as T;
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance == null) {
            instance = (T)this;
        } else if (instance != (T)this) {
            Destroy(this);
        }
    }
}