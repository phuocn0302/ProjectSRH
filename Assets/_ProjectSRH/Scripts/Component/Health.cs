using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth {get; private set;}
    [field: SerializeField] public float CurrentHealth {get; private set;}

    public AnimationClip deathAnim;
    public GameObject deathEffect;

    private Animator animator;

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
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " Died!");

        if (deathAnim) animator.Play(deathAnim.name);
        if (deathEffect) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
