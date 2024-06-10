using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour
{
    [SerializeField] protected T _prefab;       
    [SerializeField] protected int _poolCapacity = 50;
    [SerializeField] protected int _poolMaxSize = 100;    

    protected int ObjectCounter = 0;
    
    public int ObjectCounterNumber()
    {
        return ObjectCounter;
    }

    protected abstract T CreatePooledObject();    
    
    protected abstract void OnTakeFromPool(T _prefab);

    protected abstract void OnReturnToPool(T _prefab);

    protected abstract void OnDestroyObject(T _prefab);
    
}
