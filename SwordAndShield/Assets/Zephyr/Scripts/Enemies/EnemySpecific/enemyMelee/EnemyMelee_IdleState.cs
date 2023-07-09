using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee_IdleState : IdleState
{
    private EnemyMelee enemy;
    
    public EnemyMelee_IdleState(Entity entity, FiniteStateMachine stateMachine, string animName, D_IdleState stateData, EnemyMelee enemy) : base(entity, stateMachine, animName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Idle");
        enemy.turnTimer = Random.RandomRange(1f, 3f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //enemy.anim.Play("IdleEnemy");

        enemy.turnTimer -= Time.deltaTime;

        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }

        if(enemy.turnTimer < 0)
        {
            enemy.Flip();
            enemy.turnTimer = Random.RandomRange(1f, 3f);
        }

        Debug.Log(enemy.turnTimer);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
