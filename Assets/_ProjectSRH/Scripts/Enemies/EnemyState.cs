using UnityEngine;

public class EnemyState : State 
{
    protected EnemyCore core;

    public SpriteRenderer spriteRenderer => core.spriteRenderer;
    public Rigidbody2D body => core.body;
    public Collider2D bodyCollider => core.bodyCollider;
    public Collider2D hitbox => core.hitbox;
    public Collider2D hurtbox => core.hurtbox;
    public Animator animator => core.animator;
    public float moveSpeed => core.moveSpeed;

    public GhostEffect ghostEffect => core.ghostEffect;

    public void Setup(EnemyCore _core)
    {
        core = _core;
    }

}