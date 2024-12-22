using System;
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
    }

    public void Heal(float healAmount)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, MaxHealth);
        OnHealthIncreased?.Invoke();
        OnHealthChange?.Invoke(CurrentHealth);
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died!");

        if (deathAnim) animator.Play(deathAnim.name);
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
