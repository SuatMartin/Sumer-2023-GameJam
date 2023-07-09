using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_MoveState : MoveState
{
    private EnemyMelee enemy;
    
    public EnemyMelee_MoveState(Entity entity, FiniteStateMachine stateMachine, string animName, D_MoveState stateData, EnemyMelee enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Move");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        //enemy.anim.Play("WalkEnemy");

        base.LogicUpdate();

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if(isDetectingWall || isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
