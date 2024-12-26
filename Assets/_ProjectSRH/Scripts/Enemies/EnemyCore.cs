using UnityEngine;

public class EnemyCore : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    public Collider2D bodyCollider;
    public Collider2D hitbox;
    public Collider2D hurtbox;
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

    protected virtual void SelectState()
    {

    }
}
