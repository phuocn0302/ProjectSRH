using System.Collections;
using DG.Tweening;
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
        
        spriteRenderer.transform.DOScaleY(0.2f, 0.5f);
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
        
        spriteRenderer.transform.DOScaleY(1, 0.5f);
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