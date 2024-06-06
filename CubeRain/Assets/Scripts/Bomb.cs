using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{  
    private float _minDelay = 2f;
    private float _maxDelay = 5f;
    private float _timer = 0.0f;
    private Renderer _renderer;
    private float _explosionRadius = 500f;
    private float delay;
    private float _upwardsModifier = 3f;   
    private float _force = 250f;

    public event Action<Bomb> Disable;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
        WaitForSeconds wait = new WaitForSeconds(delay);

        Coroutine _coroutine = StartCoroutine(DisableBomb(wait));
    }

    void Update()
    {
        _timer += Time.deltaTime;
        float alpha = 1 - (_timer / delay);
        Color color = _renderer.material.color;
        color.a = alpha;
        _renderer.material.color = color;   
    }        

    private void BlowBomb()
    {
        Vector3 bombCenterPosition = transform.position;

        Collider[] blowColliders = Physics.OverlapSphere(bombCenterPosition, _explosionRadius);

        foreach (Collider hit in blowColliders)
        {
            Rigidbody rigidBody = hit.GetComponent<Rigidbody>();            

            if (rigidBody != null)
            {
                rigidBody.AddExplosionForce(_force, bombCenterPosition, _explosionRadius, _upwardsModifier, ForceMode.Force);
            }
        }        
    }

    private IEnumerator DisableBomb(WaitForSeconds wait)
    {
        yield return wait;
        BlowBomb();
        Disable?.Invoke(this);
    }
}
