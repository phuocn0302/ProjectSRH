using UnityEngine;
using PrimeTween;
using System.Collections;

public class MeleeBugAttackState : EnemyState
{
    [SerializeField] private AnimationClip anim;
    [SerializeField] private TweenSettings<Color> tweenSettings;
    [SerializeField] private float dashSpeed = 1500f;
    [SerializeField] private float dashTime = 0.1f;

    [SerializeField] private float restTime = 1f;

    private Collider2D hitboxArea;
    private Collider2D hurtboxArea;

    public override void Initialize()
    {
        base.Initialize();
        hurtboxArea = hurtbox.GetComponent<Collider2D>();
        hitboxArea = GetComponent<Collider2D>();
        hitboxArea.enabled = false;
    }

    public override void Enter()
    {
        if (anim) animator.Play(anim.name);

        Tween.Custom(body.linearVelocity, Vector2.zero, 0.3f, onValueChange: val => body.linearVelocity = val);
        hurtboxArea.enabled = false;
        
        Tween.Color(spriteRenderer, tweenSettings).OnComplete(() =>{
            StartCoroutine(Attack());
        }, warnIfTargetDestroyed: false);        
    }

    public override void Exit()
    {
        IsComplete = true;
    }

    private IEnumerator Attack()
    {   
        hitboxArea.enabled = true;

        Vector2 direction = directionToTarget;

        yield return Tween.Position(body.transform, 
            body.position, 
            body.position  + -direction * 0.5f, 
            0.3f, 
            Ease.InOutBounce).ToYieldInstruction();
        body.linearVelocity = Time.fixedDeltaTime * dashSpeed * direction;

        yield return new WaitForSeconds(dashTime);
        hitboxArea.enabled = false;

        while (Vector2.Distance(body.linearVelocity, Vector2.zero) > 0.1f)
        {
            body.linearVelocity *= 0.95f;
            yield return null;
        }
        hurtboxArea.enabled = true;
        yield return new WaitForSeconds(restTime);

        Exit();
    }

}
