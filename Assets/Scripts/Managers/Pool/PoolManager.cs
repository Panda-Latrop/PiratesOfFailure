using System.Collections.Generic;


internal class PoolManager<V, R> where V : IPoolObject {
    #region Variables

        private readonly List<V> objects;

        public R Prefab { get; private set; }

        public R Root { get; private set; }

        #endregion

    #region Public methods

        public PoolManager(R poolObjectInfo, R root) {
            Prefab = poolObjectInfo;
            Root = root;
            objects = new List<V>();

        }

        public void Push(V value) {
            value.OnPush();
            objects.Add(value);
        }

        public T Pop<T>() where T : V {
            T result = default(T);
            if (objects.Count > 0) {
                result = (T) objects[0];
                objects.RemoveAt(0);
                result.Create();
            }

            return result;
        }

        #endregion
}
