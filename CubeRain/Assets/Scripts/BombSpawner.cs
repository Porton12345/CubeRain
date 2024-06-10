using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    private int _currentCounter = 0;

    private ObjectPool<Bomb> _pool;    

    private void Awake()
    {
        _pool = new ObjectPool<Bomb>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    private void Update()
    {
        if(_currentCounter != Object—ounter)
        {
            ShowText();
        }            
    }

    public int CountBombs()
    {
        return Object—ounter;
    }

    protected override void ShowText()
    {
        _text.text = "C˜ÂÚ˜ËÍ ·ÓÏ· " + Object—ounter.ToString("");
        _currentCounter = Object—ounter;
    }     

    private void ReturnBombToPool(Bomb instance)
    {
        _pool.Release(instance);        
    }

    public void GetBomb(Vector3 position)
    {
        Bomb instance = _pool.Get();
        instance.transform.position = position;
    }

    protected override Bomb CreatePooledObject()
    {
        Bomb instance = Instantiate(_prefab);        
        instance.Disable += ReturnBombToPool;
        instance.gameObject.SetActive(false);
        Object—ounter++;

        return instance;
    }

    protected override void OnDestroyObject(Bomb instance)
    {
        Destroy(instance.gameObject);
    }

    protected override void OnReturnToPool(Bomb instance)
    {
        instance.gameObject.SetActive(false);        
    }

    protected override void OnTakeFromPool(Bomb instance)
    {               
        instance.gameObject.SetActive(true);        
    }
}
