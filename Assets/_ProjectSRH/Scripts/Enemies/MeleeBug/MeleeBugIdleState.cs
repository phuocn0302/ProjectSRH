using UnityEngine;

public class MeleeBugIdleState : EnemyState
{
    [SerializeField] private AnimationClip anim;
    public override void Enter()
    {
        if (anim) animator.Play(anim.name);
        Invoke(nameof(Exit), 2f);
    }

    public override void FrameUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        body.linearVelocity *= 0.95f;
    }

    public override void Exit()
    {
        IsComplete = true;
    }


}

