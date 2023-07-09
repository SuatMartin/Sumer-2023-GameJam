using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private BoxCollider2D hurtBox;
    
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public GameObject aliveGO { get; private set; }

    public Vector2 velocityWorkspace;

    private void Start()
    {
        aliveGO = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        SetVelocity(speed);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public virtual void SetVelocity(float velocity)
    {
        facingDirection = -1;
        if (transform.localScale.x < 0) facingDirection = 1;
        
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;

        //print(rb.velocity + " " + velocityWorkspace + " " + facingDirection + "  " + velocity + " VELOCITY STUFF");
    }

    public virtual void Flip()
    {
        /*
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
        */
    }
}
