using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;    
    [SerializeField] private float _repeatRate = 5f;
    [SerializeField] private int _poolCapacity = 50;
    [SerializeField] private int _poolMaxSize = 100;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(CreatePooledCube, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ReturnCubeToPool(Cube instance)
    {        
        _pool.Release(instance);
    }

    private Cube CreatePooledCube()
    {
        Cube instance = Instantiate(_cube);
        instance.Disable += ReturnCubeToPool;
        instance.gameObject.SetActive(false);    

        return instance;
    }

    private void OnTakeFromPool(Cube instance)
    {        
        instance.GetComponent<Renderer>().material.color = Color.white;
        instance.transform.position = new Vector3(Random.Range(-8, 8), 15, Random.Range(-8, 8));
        instance.RefreshCollisionStatus();        
        instance.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Cube instance)
    {
        instance.gameObject.SetActive(false);
    }

    private void OnDestroyObject(Cube instance)
    {
        Destroy(instance.gameObject);
    }
}
