using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackDuration;
    private float cooldownTimer = Mathf.Infinity;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private bool damaging = false;
    public bool isDashing;
    private bool canDash = true;

    [Header ("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    [Header ("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;



    // Start is called before the first frame update
    private void Start(){
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update() {
        if(isDashing){
            return;
        }
        horizontalInput = Input.GetAxis("Horizontal");
        //Flip Player walking
        if(horizontalInput > .01f){
            transform.localScale = new Vector3(4,4,1);
        } else if(horizontalInput < -.01f){
            transform.localScale = new Vector3(-4,4,1);
        }

        //Set animator parameters

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0){
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        body.gravityScale = 7;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if(isGrounded()){
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        } else {
            coyoteCounter -= Time.deltaTime;
        }
         if(Input.GetMouseButton(0) && canDash)  // && canAttack()
         {
            StartCoroutine(Attack());
         }
    }

    private void Jump(){
        if(coyoteCounter < 0 && jumpCounter <= 0) {return;}
        //SoundManager.instance.PlaySound(jumpSound);
            if (isGrounded()){
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            } else {
                if(coyoteCounter > 0){
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                } else {
                    if(jumpCounter > 0){
                        body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
                coyoteCounter = 0;
            }
        }

    private bool isGrounded(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return (raycastHit.collider != null);
    }

    public bool canAttack(){
        return horizontalInput == 0 && !onWall(); 
    }

    private IEnumerator Attack(){
        canDash = false;
        isDashing = true;
        damaging = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2((transform.localScale.x/4)*attackPower,0f);
        yield return new WaitForSeconds(attackDuration);
        body.gravityScale = originalGravity;
        isDashing = false;
        damaging = false;
        yield return new WaitForSeconds(attackCooldown);
        canDash = true;
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
