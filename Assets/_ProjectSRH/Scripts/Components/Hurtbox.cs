using System.Collections;
using PrimeTween;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [field: SerializeField] public Health Health {get; private set;}
    [SerializeField] private bool canBeKnockback = false;
    
    public float iframeTime = 0.3f;
    private Collider2D hurtboxArea;
    
    private void Awake()
    {   
        hurtboxArea = GetComponent<Collider2D>();
        Health = GetComponentInParent<Health>();
    }

    public void TakeDamage(float damageAmount)
    {
        Health.TakeDamage(damageAmount);
        StartCoroutine(Iframe(iframeTime));
    }

    public void TakeKnockback(Transform knockbackForce, float knockbackAmount)
    {
        if (!canBeKnockback) return;
        if (transform.parent.TryGetComponent<Rigidbody2D>(out var body)) 
        {
            Vector2 knockback = (transform.position - knockbackForce.position).normalized;

            knockback = knockback.normalized * knockbackAmount;
            Tween.Position(body.transform, (Vector2)body.transform.position + knockback, 0.5f);
        }
    }

    private IEnumerator Iframe(float _time)
    {
        float _elapsedTime = 0;
        while (_elapsedTime < _time)
        {
            hurtboxArea.enabled = false;
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        hurtboxArea.enabled = true;
    }
}