using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Entity
{
    public EnemyMelee_IdleState idleState { get; private set; }
    public EnemyMelee_MoveState moveState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;

    public override void Start()
    {
        base.Start();

        moveState = new EnemyMelee_MoveState(this, stateMachine, moveStateData, this);
    }
}
