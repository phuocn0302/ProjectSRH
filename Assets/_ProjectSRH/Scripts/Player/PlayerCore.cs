using UnityEngine;


public class PlayerCore : MonoBehaviour 
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D body;
    public Collider2D bodyCollider;
    public Collider2D hitbox;
    public Collider2D hurtbox;
    public Animator animator;   
    public float moveSpeed;
    public Vector2 moveInput;
    public Vector2 mouseDirection;

    [field: SerializeField] public Vector2 FacingDirection {get; protected set;} = Vector2.down;
    [field: SerializeField] public string FacingDirectionStr {get; protected set;} = "Down";


    protected StateMachine stateMachine;
    protected PlayerState state => (PlayerState)stateMachine.state;

    protected void SetupInstances()
    {
        stateMachine = new StateMachine();
        PlayerState[] childStates = GetComponentsInChildren<PlayerState>();
        foreach (PlayerState s in childStates)
        {
            s.Setup(this);
        }
    }

    protected virtual void SelectState()
    {

    }
}
