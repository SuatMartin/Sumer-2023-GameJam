using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_PlayerDetectedState : PlayerDetectedState
{
    private EnemyMelee enemy;

    public EnemyMelee_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animName, D_PlayerDetectedState stateData, EnemyMelee enemy) : base(entity, stateMachine, animName, stateData)
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
        base.LogicUpdate();

        if (!isPlayerInMaxAgroRange)
        {
            //enemy.idleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(enemy.idleState);
        }

        enemy.fireTimer -= Time.deltaTime;

        if (enemy.fireTimer < 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        enemy.fireTimer = Random.RandomRange(1f, 3f);
        enemy.FireArrow();
        enemy.animator.Play("ShootEnemy");
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
