using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{        
    private bool _isFirstlyCollided = true;    
    private float _minDelay = 2f;
    private float _maxDelay = 5f;

    public event Action<Cube> Disable;

    public void RefreshCollisionStatus()
    {
        _isFirstlyCollided = true;       
    }

    private void OnCollisionEnter(Collision collision)
    {        
        if(collision.gameObject.TryGetComponent(out Platform platform) && _isFirstlyCollided)
        {
              GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
              _isFirstlyCollided = false;

              float delay = Random.Range(_minDelay, _maxDelay);
              WaitForSeconds wait = new WaitForSeconds(delay);

              Coroutine _coroutine = StartCoroutine(DisableCube(wait));                         
        }            
    }

    private IEnumerator DisableCube(WaitForSeconds wait)
    {        
        yield return wait;        
        Disable?.Invoke(this);
    }
}
