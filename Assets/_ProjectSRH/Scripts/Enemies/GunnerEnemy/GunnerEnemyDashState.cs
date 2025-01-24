using System.Collections;
using UnityEngine;

public class GunnerEnemyDashState : EnemyState
{
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private bool canDash = true;

    public override void Enter()
    {
        if (canDash)
            StartCoroutine(Dash());
        else
            Exit();
    }

    private IEnumerator StartCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator Dash()
    {
        StartCoroutine(StartCooldown());
        StartCoroutine(ghostEffect.ShowGhost(5, dashTime));
        
        Vector2 direction = Random.insideUnitCircle.normalized;
        body.linearVelocity = Time.fixedDeltaTime * moveSpeed * 5 * direction;
        yield return new WaitForSeconds(dashTime);
        body.linearVelocity = Vector2.zero;
        
        Exit();
    }

}
