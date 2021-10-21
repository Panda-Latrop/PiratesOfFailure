
internal interface IPoolObject {
    int PoolType { get; }
    void Create();
    void OnPush();
    void FailedPush();
}
