using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private BombSpawner _bombSpawner;    
       
    private ObjectPool<Cube> _pool;   

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    private void Start()
    {       
        WaitForSeconds wait = new WaitForSeconds(_repeatRate);
        StartCoroutine(CreateCubeRain(wait));
    }   

    private void GetCube()
    {
        _pool.Get();
    }

    private void ReturnCubeToPool(Cube instance)
    {
        _pool.Release(instance);
        _bombSpawner.GetBomb(instance.transform.position);
    }     

    protected override Cube CreatePooledObject()
    {
        Cube instance = Instantiate(_prefab);
        instance.Disable += ReturnCubeToPool;
        instance.gameObject.SetActive(false);
        ObjectCounter++;        

        return instance;
    }    

    protected override void OnDestroyObject(Cube instance)
    {
        Destroy(instance.gameObject);
    }

    protected override void OnReturnToPool(Cube instance)
    {
        instance.gameObject.SetActive(false);        
    }

    protected override void OnTakeFromPool(Cube instance)
    {
        instance.TryGetComponent<Renderer>(out Renderer renderer);
        renderer.material.color = Color.white;
        instance.transform.position = new Vector3(Random.Range(-8, 8), 15, Random.Range(-8, 8));
        instance.RefreshCollisionStatus();
        instance.gameObject.SetActive(true);        
    }

    private IEnumerator CreateCubeRain(WaitForSeconds wait)
    {
        while (true)
        {
            yield return wait;
            GetCube();
        }        
    }
}
