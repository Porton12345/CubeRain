using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{ 
    [SerializeField] protected T Prefab;       
    [SerializeField] protected int _poolCapacity = 50;
    [SerializeField] protected int _poolMaxSize = 100;    

    protected int ObjectCounter = 0;    
    protected ObjectPool<T> Pool;
       
    public int ObjectCounterNumber()
    {
        return ObjectCounter;
    }

    protected void Awake()
    {
        Pool = new ObjectPool<T>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    protected abstract T CreatePooledObject();    

    protected void OnDestroyObject(T instance)
    {
        Destroy(instance.gameObject);
    }

    protected void OnReturnToPool(T instance)
    {
        instance.gameObject.SetActive(false);
    }

    protected virtual void OnTakeFromPool(T instance)
    {
        instance.gameObject.SetActive(true);
    }       
}
