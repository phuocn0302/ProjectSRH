using UnityEngine;

public class PlayerState : State
{
    protected PlayerCore core;

    public SpriteRenderer spriteRenderer => core.spriteRenderer;
    public Rigidbody2D body => core.body;
    public Collider2D bodyCollider => core.bodyCollider;
    public Collider2D hitbox => core.hitbox;
    public Collider2D hurtbox => core.hurtbox;
    public Animator animator => core.animator;
    public float moveSpeed => core.moveSpeed;
    public Vector2 moveInput => core.moveInput;
    public Vector2 mouseDirection => core.mouseDirection;
    public GhostEffect ghostEffect => core.ghostEffect;

    
    public Vector2 FacingDirection => core.FacingDirection;
    public string FacingDirectionStr => core.FacingDirectionStr;

    
    
    public void Setup(PlayerCore _core)
    {
        core = _core;
    }


}