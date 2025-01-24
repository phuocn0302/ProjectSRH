using UnityEngine;
using PrimeTween;

public class GunnerEnemy : EnemyCore
{   
    [field: SerializeField] public EnemyState IdleState;
    [field: SerializeField] public EnemyState DashState;
    [field: SerializeField] public EnemyState AttackState;

    public TargetRangeCheck targetRangeCheck;
    private bool isTargetInRange;

    private void Awake()
    {
        SetupInstances();
        SetupComponent();

        targetRangeCheck.target = this.target;

        Tween.Scale(this.transform, 0f, 1f, 0.5f);
        stateMachine.SetState(AttackState);
    }

    private void OnEnable()
    {
        if (targetRangeCheck)
            targetRangeCheck.OnTargetEnter += HandleTargetEnter;
    }

    private void OnDisable()
    {
        if (targetRangeCheck)
            targetRangeCheck.OnTargetEnter -= HandleTargetEnter;
    }

    private void Update()
    {
        if (state.IsComplete)
        {
            SelectState();
        }
        state.FrameUpdate();
        HandleSpriteFlip();
    }

    private void FixedUpdate()
    {
        state.PhysicsUpdate();
    }

    protected override void SelectState()
    {
        if (isTargetInRange)
        {
            stateMachine.SetState(DashState, true);
            return;
        }
        if (target)
        {
            stateMachine.SetState(AttackState, true);
            return;
        }
        stateMachine.SetState(IdleState);
    }

    private void HandleSpriteFlip()
    {
        if (GetDirectionToTarget().x < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (GetDirectionToTarget().x > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleTargetEnter(bool _status)
    {
        isTargetInRange = _status;
    }
}
