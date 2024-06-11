using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{      
    private void ReturnBombToPool(Bomb instance)
    {
        Pool.Release(instance);        
    }

    public void GetBomb(Vector3 position)
    {
        Bomb instance = Pool.Get();
        instance.transform.position = position;
    }

    protected override Bomb CreatePooledObject()
    {
        Bomb instance = Instantiate(Prefab);        
        instance.Disable += ReturnBombToPool;
        instance.gameObject.SetActive(false);
        ObjectCounter++;

        return instance;
    }
}
