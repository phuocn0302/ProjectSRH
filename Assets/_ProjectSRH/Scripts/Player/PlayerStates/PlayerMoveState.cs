using System;
using UnityEngine;

public class PlayerMoveState : PlayerState 
{
    public override void Enter()
    {

    }

    public override void FrameUpdate()
    {
        String animName = "Run" + ((FacingDirectionStr == "Left") ? "Right" : FacingDirectionStr);
        animator.Play(animName);

        if (body.linearVelocity == Vector2.zero) Exit();
    }

    public override void PhysicsUpdate()
    {
        body.linearVelocity = Time.fixedDeltaTime * moveSpeed * moveInput; 
    }

    public override void Exit()
    {
        IsComplete = true;
    }    
}
