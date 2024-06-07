using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private TextMeshProUGUI _activeObjectCountText;

    private int _activeObjectCounter = 0;
    private ObjectPool<Cube> _pool;   

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(CreatePooledObject, OnTakeFromPool, OnReturnToPool, OnDestroyObject, false, _poolCapacity, _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void Update()
    {
        _text.text = "Cчетчик кубов " + _counter.ToString("");
        _activeObjectCounter = _counter + _bombSpawner.CountBombs();
        _activeObjectCountText.text = "Cчетчик активных объектов " + _activeObjectCounter.ToString("");
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
        _counter++;        

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
        instance.GetComponent<Renderer>().material.color = Color.white;
        instance.transform.position = new Vector3(Random.Range(-8, 8), 15, Random.Range(-8, 8));
        instance.RefreshCollisionStatus();
        instance.gameObject.SetActive(true);        
    }
}
