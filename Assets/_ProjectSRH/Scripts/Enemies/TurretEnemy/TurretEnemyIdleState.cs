using UnityEngine;

public class TurretEnemyIdleState : EnemyState
{

	public AnimationClip anim;

    public override void Enter()
    {
        if (anim)
            animator.Play(anim.name);
    }

    public override void PhysicsUpdate()
    {
        body.linearVelocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }
}