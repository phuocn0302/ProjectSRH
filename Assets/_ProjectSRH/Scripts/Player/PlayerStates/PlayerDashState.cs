using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState 
{
    public AnimationClip anim;

    public float dashSpeed;
    public float dashCooldown;
    public float dashTime;

    [field: SerializeField] public bool canDash = true;

    public override void Enter()
    {
        if (!canDash)
        {
            Exit();
            return;
        }
        
        if (anim) animator.Play(anim.name);
        
        StartCoroutine(Dash());
    }


    public override void Exit()
    {
        hurtbox.enabled = true;
        IsComplete = true;
    }    

    public IEnumerator Dash()
    {
        canDash = false;
        Vector2 dashDirection = moveInput;
        hurtbox.enabled = false;

        body.linearVelocity = Time.fixedDeltaTime * dashSpeed * dashDirection;
        yield return new WaitForSeconds(dashTime);
    
        body.linearVelocity = Time.fixedDeltaTime * dashSpeed * dashDirection;

        StartCoroutine(DashCooldown());
        Exit();
    }

    public IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}