using UnityEngine;
using PrimeTween;

public class MeleeBug : EnemyCore
{

    [field: SerializeField] public EnemyState IdleState {get; private set;}
    [field: SerializeField] public EnemyState ChaseState {get; private set;}
    [field: SerializeField] public EnemyState AttackState {get; private set;}

    public TargetRangeCheck targetRangeCheck;
    private bool isTargetEnter;
    public EnemyState CurrrentState => state;
    private void Awake() 
    {
        SetupInstances();
        SetupComponent();
        stateMachine.SetState(IdleState);
        Tween.Scale(this.transform, 0f, 1f, 0.5f);

        hurtbox = GetComponentInChildren<Hurtbox>();
        targetRangeCheck = GetComponentInChildren<TargetRangeCheck>();
        targetRangeCheck.target = this.target;
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
        UpdateSpriteFlip();
        if (state.IsComplete)
        {
            SelectState();
        }
        state.FrameUpdate();
    }

    private void FixedUpdate() 
    {
        state.PhysicsUpdate();
    }

    protected override void SelectState()
    {

        if (isTargetEnter)
        {
            stateMachine.SetState(AttackState, true);
            return;
        }

        if (target) 
        {
            stateMachine.SetState(ChaseState);
            return;
        }
        

        stateMachine.SetState(IdleState);
    }

    private void UpdateSpriteFlip()
    {
        if (body.linearVelocityX < 0 && !spriteRenderer.flipX)
        {
            spriteRenderer.flipX = true;
        }
        else if (body.linearVelocityX > 0 && spriteRenderer.flipX)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void HandleTargetEnter(bool _isTargetEnter)
    {
        isTargetEnter = _isTargetEnter;
        if (!_isTargetEnter) return;
        stateMachine.SetState(AttackState);
    }
}