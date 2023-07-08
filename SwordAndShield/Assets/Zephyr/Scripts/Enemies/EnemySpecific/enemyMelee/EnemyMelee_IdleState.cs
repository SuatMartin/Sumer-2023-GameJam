using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_IdleState : IdleState
{
    private EnemyMelee enemy;
    
    public EnemyMelee_IdleState(Entity entity, FiniteStateMachine stateMachine, D_IdleState stateData, EnemyMelee enemy) : base(entity, stateMachine, stateData)
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
