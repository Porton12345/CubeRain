using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{   
    public delegate void OnDisableCallback(Cube instance);
    public OnDisableCallback Disable;
    
    private bool _isFirstlyCollided = true;
    private Coroutine _coroutine;
    private float _minDelay = 2f;
    private float _maxDelay = 5f;

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
                
                _coroutine = StartCoroutine(DisableCube(wait));                         
        }            
    }

    private IEnumerator DisableCube(WaitForSeconds wait)
    {        
        yield return wait; 
        Disable?.Invoke(this);            
    }
}
