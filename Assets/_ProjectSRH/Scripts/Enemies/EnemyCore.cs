using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public GameObject target;
    public Health health;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    public Collider2D bodyCollider;
    public Hurtbox hurtbox;
    public Animator animator;   
    public float moveSpeed;
    public GhostEffect ghostEffect;
    protected StateMachine stateMachine;

    protected EnemyState state => (EnemyState)stateMachine.state;

    
    protected void SetupInstances()
    {
        stateMachine = new StateMachine();
        EnemyState[] childStates = GetComponentsInChildren<EnemyState>();
        foreach (EnemyState s in childStates)
        {
            s.Setup(this);
        }
    }

    protected void SetupComponent()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        TryGetComponent<Health>(out health);
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<Rigidbody2D>(out body);
        TryGetComponent<Collider2D>(out bodyCollider);
        TryGetComponent<Animator>(out animator);
        TryGetComponent<GhostEffect>(out ghostEffect);
        
    }

    protected virtual void SelectState()
    {

    }

    public Vector2 GetTargetPosition()
    {
        if (!target) return this.transform.position;
        return target.transform.position;
    }

    public Vector2 GetDirectionToTarget()
    {
        if (!target) return Vector2.zero;
        return (target.transform.position - this.transform.position).normalized;
    }
}
