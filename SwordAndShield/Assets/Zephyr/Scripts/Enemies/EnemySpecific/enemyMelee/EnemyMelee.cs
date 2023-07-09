using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Entity
{
    public EnemyMelee_IdleState idleState { get; private set; }
    public EnemyMelee_PlayerDetectedState playerDetectedState { get; private set; }

    //TEMPORARY
    [SerializeField]
    protected Projectile arrow;

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;

    public float fireTimer { get; set; }
    public float turnTimer { get; set; }

    public override void Start()
    {
        base.Start();

        idleState = new EnemyMelee_IdleState(this, stateMachine, "IdleEnemy", idleStateData, this);
        playerDetectedState = new EnemyMelee_PlayerDetectedState(this, stateMachine, "", playerDetectedStateData, this);

        stateMachine.Initialize(idleState);
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
