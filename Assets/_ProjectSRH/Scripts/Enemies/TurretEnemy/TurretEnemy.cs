using PrimeTween;
using UnityEngine;

public class TurretEnemy : EnemyCore
{
    [field: SerializeField] public EnemyState IdleState;
    [field: SerializeField] public EnemyState ShootState;

    public TargetRangeCheck targetRangeCheck;
    private bool isTargetInRange;

    private void Awake() 
    {
        SetupInstances();
        SetupComponent();

        targetRangeCheck.target = this.target;

        Tween.Scale(this.transform, 0f, 1f, 0.5f);
        stateMachine.SetState(IdleState);
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
        state.FrameUpdate();
    }

    private void FixedUpdate()
    {
        state.PhysicsUpdate();
    }

    private void HandleTargetEnter(bool _status)
    {
        isTargetInRange = _status;
        if (isTargetInRange)
        {
            stateMachine.SetState(ShootState);
        }
        else
        {
            stateMachine.SetState(IdleState);
        }
    }
}
