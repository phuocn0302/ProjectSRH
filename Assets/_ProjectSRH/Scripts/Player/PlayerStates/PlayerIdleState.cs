using System;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Enter()
    {
        body.linearVelocity = Vector2.zero;
    }

    public override void FrameUpdate()
    {
        String animName = "Idle" + ((FacingDirectionStr == "Left") ? "Right" : FacingDirectionStr);
        animator.Play(animName);

        if (moveInput != Vector2.zero)
        {
            Exit();
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void Exit()
    {
        IsComplete = true;
    }
}
