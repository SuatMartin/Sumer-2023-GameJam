using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBash : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackDuration;
    private Rigidbody2D body;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private BoxCollider2D boxCollider;
    private bool damaging = false;
    // Start is called before the first frame update
    void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    void Update(){
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()){
            Attack();
        }
        if(cooldownTimer >= attackDuration){
            damaging = false;
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack(){
        cooldownTimer = 0;
        if(transform.localScale.x > 0){
            body.AddForce(transform.right * attackPower);
            damaging = true;
        } else {
           body.AddForce((transform.right * attackPower)*-1);
           damaging = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Enemy"){
            if(damaging){
            collision.GetComponent<Health>().takeDamage(1);
            } else {
            boxCollider.GetComponent<Health>().takeDamage(1);
            }
        }
    }
}
