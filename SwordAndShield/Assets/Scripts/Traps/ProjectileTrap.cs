using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    
    private float cooldownTimer;
    // Start is called before the first frame update

    private void Attack(){
        cooldownTimer = 0;
        projectiles[FindArrow()].transform.position = firePoint.position;
        projectiles[FindArrow()].GetComponent<TrapProjectile>().ActivateProjectile();
    }

    private int FindArrow(){
        for(int i = 0; i < projectiles.Length; ++i){
            if(!projectiles[i].activeInHierarchy){
                return i;
            }
        }
        return 0;
    }

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        cooldownTimer += Time.deltaTime;

        if(cooldownTimer >= attackCooldown){
            Attack();
        }
    }
}

