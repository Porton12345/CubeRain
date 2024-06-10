using System;
using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{  
    private float _minDelay = 2f;
    private float _maxDelay = 5f;    
    private Renderer _renderer;
    private float _explosionRadius = 500f;
    private float _delay;
    private float _upwardsModifier = 3f;   
    private float _force = 250f;
    private Color _color;

    public event Action<Bomb> Disable;
    
    private void OnEnable()
    {
        _renderer = GetComponent<Renderer>();
        _color = Color.black;
        _color.a = 1;
        _renderer.material.color = _color;
        _delay = UnityEngine.Random.Range(_minDelay, _maxDelay);
        WaitForSeconds wait = new WaitForSeconds(_delay);

        StartCoroutine(DisableBomb(wait));
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
        StartCoroutine(FadeBomb());
        yield return wait;        
        BlowBomb();        
        Disable?.Invoke(this);
    }

    private IEnumerator FadeBomb()
    {                
        int steps = 1020;                

        for(int i = 0; i < steps; i++) 
        {            
            _color.a = 1f-((1f/steps) * i);
            _renderer.material.color = _color;
            yield return null;
        }
    }
}
