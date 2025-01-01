using UnityEngine;

public class GunnerEnemyIdleState : EnemyState
{
    public override void PhysicsUpdate()
    {
        body.linearVelocity = Vector2.zero;
    }
}
