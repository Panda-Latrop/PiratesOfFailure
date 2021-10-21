using System.Collections.Generic;

using UnityEngine;


 internal sealed class UnityPoolManager : DestroyableSingleton<UnityPoolManager> {
     #region Variables

        [SerializeField] private UnityPoolObject[] poolsInfo;

        private readonly Dictionary<int, PoolManager<UnityPoolObject, GameObject>> pools =
            new Dictionary<int, PoolManager<UnityPoolObject, GameObject>>();

        #endregion

     #region Unity lifecycle

     protected override void Awake() {
         base.Awake();

         foreach (var poolObjectInfo in poolsInfo) {
             if (poolObjectInfo != null && !pools.ContainsKey(poolObjectInfo.PoolType)) {
                 var root = CreateRoot(poolObjectInfo.PoolType.ToString());
                var prefab = root.AddChild(poolObjectInfo.gameObject);
                 var pool = new PoolManager<UnityPoolObject, GameObject>(prefab, root);
                 pools.Add(poolObjectInfo.PoolType, pool);
                 var obj = Pop<UnityPoolObject>(poolObjectInfo.PoolType, false);
                 Push(obj);
             }
         }
     }
     
    #endregion

    #region Public methods

    internal void Push(UnityPoolObject poolObject) {
            if (pools.ContainsKey(poolObject.PoolType)) {
                var pool = pools[poolObject.PoolType];
                pool.Push(poolObject);
                poolObject.transform.parent = pool.Root.transform;
            }
        }

        internal T Pop<T>(int poolType, GameObject root, bool activate = true) where T : UnityPoolObject {
            var obj = Pop<T>(poolType, activate);
            if (obj != null) {
                obj.transform.parent = root.transform;
                obj.SetTransform(Vector3.zero, Quaternion.identity, Vector3.one);
            }
            return obj;
        }

        internal T Pop<T>(int poolType, bool activate = true) where T : UnityPoolObject {
            if (pools.ContainsKey(poolType)) {
                T result = pools[poolType].Pop<T>() ?? CreateObject<T>(pools[poolType].Prefab, pools[poolType].Root);
                result.gameObject.SetActive(activate);
                return result;
            }

            return null;
        }

        #endregion

     #region Private methods

        private GameObject CreateRoot(string rootName) {
            GameObject root = gameObject.AddChild();

            root.name = rootName;

            return root;
        }

        private static T CreateObject<T>(GameObject prefab, GameObject parent)
            where T : UnityPoolObject {
            GameObject go = parent.AddChild(prefab);

            T result = go.GetComponent<T>();

            return result;
        }

        #endregion
 }

public static class CustomTools {
    public static GameObject AddChild(this GameObject parent, GameObject prefab)
     {
         GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
         UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
         if (go != null && parent != null)
         {
             Transform t = go.transform;
             t.parent = parent.transform;
             t.localPosition = Vector3.zero;
             t.localRotation = Quaternion.identity;
             t.localScale = Vector3.one;
          //   go.layer = parent.layer;
         }
         return go;
     }
    static public GameObject AddChild(this GameObject parent) { return AddChild(parent, true); }
    

    static public GameObject AddChild(this GameObject parent, bool undo)
    {
        GameObject go = new GameObject();
#if UNITY_EDITOR
        if (undo) UnityEditor.Undo.RegisterCreatedObjectUndo(go, "Create Object");
#endif
        if (parent != null)
        {
            Transform t = go.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
           // go.layer = parent.layer;
        }
        return go;
    }
}