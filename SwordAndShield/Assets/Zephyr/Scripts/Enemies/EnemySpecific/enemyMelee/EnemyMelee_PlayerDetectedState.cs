using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_PlayerDetectedState : PlayerDetectedState
{
    private EnemyMelee enemy;
    float fireTimer;

    public EnemyMelee_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, D_PlayerDetectedState stateData, EnemyMelee enemy) : base(entity, stateMachine, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("PlayerDetected");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        Debug.Log("logic update");

        base.LogicUpdate();

        if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }

        fireTimer -= Time.deltaTime;

        if (fireTimer < 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        fireTimer = Random.RandomRange(1f, 3f);
        enemy.FireArrow();    
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
