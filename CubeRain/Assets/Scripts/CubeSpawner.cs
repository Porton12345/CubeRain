using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 0.5f;
    [SerializeField] private BombSpawner _bombSpawner;               

    private void Start()
    {       
        WaitForSeconds wait = new WaitForSeconds(_repeatRate);
        StartCoroutine(CreateCubeRain(wait));
    }   

    private void GetCube()
    {
        Pool.Get();
    }

    private void ReturnCubeToPool(Cube instance)
    {
        Pool.Release(instance);
        _bombSpawner.GetBomb(instance.transform.position);
    }     

    protected override Cube CreatePooledObject()
    {
        Cube instance = Instantiate(Prefab);
        instance.Disable += ReturnCubeToPool;
        instance.gameObject.SetActive(false);
        ObjectCounter++;        

        return instance;
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
