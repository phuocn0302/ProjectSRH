using System;
using PrimeTween;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth {get; private set;}
    [field: SerializeField] public float CurrentHealth {get; private set;}

    public AnimationClip deathAnim;
    public GameObject deathEffect;

    private Animator animator;
    public event Action<float> OnHealthChange;
    public event Action OnHealthDepleted;
    public event Action OnHealthIncreased;
    public event Action OnObjectDie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start() 
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damageAmount, 0, MaxHealth);
        OnHealthDepleted?.Invoke();
        OnHealthChange?.Invoke(CurrentHealth);

        if (CurrentHealth == 0)
            Die();

        TryGetComponent<Rigidbody2D>(out var body);
        if (body) Tween.Scale(body.transform, 1.1f, 1f, 0.5f);
    }

    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, MaxHealth);
        OnHealthIncreased?.Invoke();
        OnHealthChange?.Invoke(CurrentHealth);
    }

    private void Die()
    {
        OnObjectDie?.Invoke();

        if (deathAnim) animator.Play(deathAnim.name);
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject, deathAnim? deathAnim.length : 0);
        
    }
}
