using UnityEngine;

public class MeleeBugChaseState : EnemyState
{
    [SerializeField] private AnimationClip anim;
    public override void Enter()
    {
        if (anim) animator.Play(anim.name);
    }

    public override void PhysicsUpdate()
    {
        body.linearVelocity = Time.fixedDeltaTime * moveSpeed * directionToTarget;
    }

    public override void Exit()
    {
        IsComplete = true;
    }
    
}