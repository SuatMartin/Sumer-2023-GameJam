using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlayerMovement : GeneralPlayerControls
{


    // Start is called before the first frame update
    private void Start(){
    }

    // Update is called once per frame
    private void Update() {


        GetMovement();
    }



    private bool onWall(){
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return (raycastHit.collider != null);
    }



 
}
