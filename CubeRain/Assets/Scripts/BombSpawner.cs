using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{   
    private ObjectPool<Bomb> _pool;    

    private void Awake()
    {
        _pool = new ObjectPool<Bomb>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    private void Update()
    {
        _text.text = "C������ ���� " + _counter.ToString("");
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
        instance.GetComponent<Renderer>().material.color = Color.black;
        instance.Disable += ReturnBombToPool;
        instance.gameObject.SetActive(false);
        _counter++;

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
        instance.GetComponent<Renderer>().material.color = Color.black;
        instance.gameObject.SetActive(true);
    }
}
