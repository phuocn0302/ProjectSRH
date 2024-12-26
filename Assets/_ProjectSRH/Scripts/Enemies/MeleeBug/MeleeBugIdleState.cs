using UnityEngine;

public class MeleeBugIdleState : EnemyState
{
    public override void Enter()
    {

    }

    public override void FrameUpdate()
    {
        
    }

    public override void PhysicsUpdate()
    {
        body.linearVelocity = Vector2.zero;
    }

    public override void Exit()
    {
        IsComplete = true;
    }


}

