using UnityEngine;

public class UnityPoolObject : MonoBehaviour, IPoolObject {
    #region Variables

        [SerializeField] private int poolType;

        protected Transform _transform;

        internal Transform CachedTransform {
            get { return _transform??(_transform = transform); }
        }

        #endregion

    #region Unity lifecycle

        #endregion

    #region IPoolObject

        public int PoolType {
            get { return poolType; }
        }

        public virtual void Create() {
            //InPool = false;
        }

        public virtual void OnPush() {
            gameObject.SetActive(false);
        }

        public void FailedPush() {
            Destroy(gameObject);
        }

        #endregion

    #region Public methods

        internal virtual void SetTransform(Vector3 position, Quaternion rotation, Vector3 scale, bool local = true) {
            if (_transform == null) {
                _transform = transform;
            }
            if (local) {
                _transform.localPosition = position;
                _transform.localRotation = rotation;
                _transform.localScale = scale;
            } else {
                _transform.position = position;
                _transform.rotation = rotation;
            }
        }

        internal virtual void Push() {
            UnityPoolManager.Instance.Push(this);
        }

        #endregion
}