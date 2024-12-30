using UnityEngine;
using PrimeTween;
using System;

public class GunnerEnemy : EnemyCore
{   
    [field: SerializeField] public EnemyState IdleState;
    [field: SerializeField] public EnemyState RunState;
    [field: SerializeField] public EnemyState AttackState;

    private void Awake()
    {
        SetupInstances();
        SetupComponent();
        Tween.Scale(this.transform, 0f, 1f, 0.5f);
        stateMachine.SetState(AttackState);
    }
}
