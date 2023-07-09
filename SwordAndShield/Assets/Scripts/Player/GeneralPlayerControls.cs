using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GeneralPlayerControls : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackPower;
    [SerializeField] private float attackDuration;
    private float cooldownTimer = Mathf.Infinity;

    private float wallJumpCooldown;
    private bool damaging = false;
    public bool isDashing;
    private bool canDash = true;

    [SerializeField] public float speed;
    [SerializeField] public float jumpPower;

    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;

    [Header("Coyote Time")]
    [SerializeField] public float coyoteTime;
    public float coyoteCounter;

    [Header("Multiple Jumps")]
    [SerializeField] public int extraJumps;
    public int jumpCounter;

    public Animator anim;
    public BoxCollider2D boxCollider;

    public float horizontalInput;
    public Rigidbody2D body;

    public VisualEffect walk;
    public VisualEffect land;

    public bool isSword;

    public Vector2 boxSize;
    public Transform parryPoint;
    public VisualEffect parry;

    public VisualEffect bash;

    bool grounded, prevGrounded;

    public float dustHeight;

    bool isParrying;

    public int keyCount;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovement();
    }

    public void GetKey()
    {
        keyCount++;
    }
    
    public void GetMovement()
    {

        if (isDashing) return;

        print(isDashing + " " + isParrying);
        
        grounded = isGrounded();

        horizontalInput = Input.GetAxis("Horizontal");
        //Flip Player walking
        if (horizontalInput > .01f)
        {
            if (!isParrying) SetAnimState("run");

            print("move right?");
            transform.localScale = new Vector3(4, 4, 1);
        }
        else if (horizontalInput < -.01f)
        {
            print("move left?>");
            if (!isParrying) SetAnimState("run");
            transform.localScale = new Vector3(-4, 4, 1);
        }
        else
        {
            if (!isParrying) SetAnimState("idle");
        }

        if (isParrying) return;

        //Set animator parameters

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        body.gravityScale = 7;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (isGrounded())
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))  // && canAttack()
        {
            if (isSword) StartCoroutine(Parry());
            else StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switchPlayer();
        }

        if (!prevGrounded && grounded)
        {
            print("I just landed");
            Land();
        }

        prevGrounded = grounded;
    }

    public void SetAnimState(string state)
    {
        switch (state)
        {
            case "idle":
                if (isSword) anim.Play("IdleSword");
                else anim.Play("IdleShield");
                break;
            case "jump":
                if (isSword) anim.Play("JumpSword");
                else anim.Play("JumpShield");
                break;
            case "run":
                if (isSword) anim.Play("RunSword");
                else anim.Play("RunShield");
                break;
            case "attack":
                if (isSword) anim.Play("Parry");
                else anim.Play("Dash");
                break;
        }
    }

    public void switchPlayer()
    {
        print("SWAP CHARACTERS");
        // depending on teh state switch to the other approprirate state

        if (isSword)
        {
            if (grounded)
            {
                print("1");
                anim.Play("IdleShield");
            }
            else
            {
                print("2");
                anim.Play("JumpShield");
            }
        }
        else
        {
            if (grounded)
            {
                print("3");
                anim.Play("IdleSword");
            }
            else {
                anim.Play("JumpSword"); print("4");
            }

        }

        isSword = !isSword;
    }


    IEnumerator Parry()
    {
        print("parry");
        anim.Play("Parry");

        isParrying = true;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(parryPoint.position, boxSize, 0f);

        // Check if any colliders were found
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                // if hit arrow, make arrow flip away
                if (!collider.name.Contains("Player"))
                {
                    if (collider.name.Contains("arrow")) // this is placeholder
                    {
                        print("I HIT AN ARROW");

                        // make arrow flip away
                        //collider.gameObject.GetComponent<Projectile>().FlipOut();
                    }
                    // Do something with the collider, e.g., access its GameObject or apply some logic
                    Debug.Log("Collision detected with: " + collider.gameObject.name);

                    parry.Play();
                }
            }
        }

        yield return new WaitForSeconds(.5f);

        isParrying = false;
        
    }

    void HitByArrow(GameObject arrow)
    {
        //arrow.GetComponent<Projectile>().Kill();

        LoseHealth();
    }

    void LoseHealth()
    {
        // send message to UI things
    }

    private IEnumerator Dash()
    {
        anim.Play("Dash");

        canDash = false;
        isDashing = true;
        damaging = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.velocity = new Vector2((transform.localScale.x / 4) * attackPower, 0f);
        yield return new WaitForSeconds(attackDuration);
        body.gravityScale = originalGravity;
        isDashing = false;
        damaging = false;
        yield return new WaitForSeconds(attackCooldown);
        canDash = true;
    }

    private void Jump()
    {
        print(jumpCounter);
        if (coyoteCounter < 0 && jumpCounter <= 0) { return; }
        //SoundManager.instance.PlaySound(jumpSound);
        if (isGrounded())
        {
            if (isSword) anim.Play("JumpSword");
            else anim.Play("JumpShield");
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
        {
            if (coyoteCounter > 0)
            {
                if (isSword) anim.Play("JumpSword");
                else anim.Play("JumpShield");
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if (jumpCounter > 0)
                {
                    if (isSword) anim.Play("JumpSword");
                    else anim.Play("JumpShield");
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                    jumpCounter--;
                }
            }
            coyoteCounter = 0;
        }
    }

    void Land()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        if (isSword) anim.Play("IdleSword");
        else anim.Play("IdleShield");
        Vector2 spawnLocation = raycastHit.point;
        land.SetVector2("DustSpawn", spawnLocation);
        land.Play();
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && !onWall();
    }

    public bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return (raycastHit.collider != null);
    }

    public bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("arrow"))
        {
            HitByArrow(collision.gameObject);
        }

        if (collision.tag == "Enemy")
        {
            if (damaging)
            {
                collision.GetComponent<Health>().takeDamage(1);
            }
            else
            {
                boxCollider.GetComponent<Health>().takeDamage(1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a wireframe box to visualize the collision box in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(parryPoint.position, boxSize);
    }

}
