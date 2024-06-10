using System;
using System.Collections;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{  
    private float _minDelay = 2f;
    private float _maxDelay = 5f;
    private float _timer = 0.0f;
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

    private void Update()
    {
        _timer += Time.deltaTime;        
        float alpha = Mathf.Log(_delay/_timer);        
        _color = _renderer.material.color;
        _color.a = alpha;
        _renderer.material.color = _color;   
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
