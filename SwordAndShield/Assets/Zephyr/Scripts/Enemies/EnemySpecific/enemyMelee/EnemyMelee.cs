using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Entity
{
    public EnemyMelee_IdleState idleState { get; private set; }
    public EnemyMelee_MoveState moveState { get; private set; }
    public EnemyMelee_PlayerDetectedState playerDetectedState { get; private set; }

    //TEMPORARY
    [SerializeField]
    protected Projectile arrow;

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;

    public Animator anim;

    public override void Start()
    {
        base.Start();

        moveState = new EnemyMelee_MoveState(this, stateMachine, moveStateData, this);
        idleState = new EnemyMelee_IdleState(this, stateMachine, idleStateData, this);
        playerDetectedState = new EnemyMelee_PlayerDetectedState(this, stateMachine, playerDetectedStateData, this);

        stateMachine.Initialize(moveState);
    }

    //TEMPORARY
    public void FireArrow()
    {
        //anim.Play("ShootEnemy");

        Projectile newArrow = Instantiate(arrow);
        newArrow.transform.position = wallCheck.transform.position;

        if(facingDirection == -1)
        {
            newArrow.Flip();
        }
    }
}
