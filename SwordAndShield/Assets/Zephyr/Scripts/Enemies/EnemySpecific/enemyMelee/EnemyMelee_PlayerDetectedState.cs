using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_PlayerDetectedState : PlayerDetectedState
{
    private EnemyMelee enemy;
    
    public EnemyMelee_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, D_PlayerDetectedState stateData, EnemyMelee enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
