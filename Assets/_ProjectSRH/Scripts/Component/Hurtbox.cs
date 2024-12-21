using System.Collections;
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
            Vector2 knockback = transform.position - knockbackForce.position;
            if (body.linearVelocity != Vector2.zero)
                knockback -= body.linearVelocity;


            knockback = knockback.normalized * knockbackAmount;
            body.AddForce(knockback, ForceMode2D.Impulse); 
        }
    }

    private IEnumerator Iframe(float _time)
    {
        hurtboxArea.enabled = false;
        yield return new WaitForSeconds(_time);
        hurtboxArea.enabled = true;
    }
}