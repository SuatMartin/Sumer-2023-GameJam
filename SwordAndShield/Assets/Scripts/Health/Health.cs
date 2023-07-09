using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;
    // Start is called before the first frame update
    void Start(){
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float _damage){
        if(invulnerable){return;}
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0){
            anim.SetTrigger("hurt"); // we need a hurt animation, but not relly we should just start the coroutine
            StartCoroutine(Invulnerability());
        } else {
            if(!dead){
                foreach(Behaviour component in components){
                    component.enabled = false;
                }
                anim.SetBool("grounded", true);
                anim.SetTrigger("die");
                dead = true;
                print("YOU DIED");
                spriteRend.color = new Color(1, 0, 0, 0.5f);
                GetComponent<GeneralPlayerControls>().Dead();
            }
        }
    }

    public void AddHealth(float _value){
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn(){
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());
        foreach(Behaviour component in components){
            component.enabled = true;
        }
    }

    private IEnumerator Invulnerability(){ // so does this run?
        print("INVICIBLE");
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8,9, true);
        for(int i = 0; i < numberOfFlashes; ++i){
            spriteRend.color = new Color(1,0,0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(8,9, false);
        invulnerable = false;
    }

    private void Deactivate(){
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
