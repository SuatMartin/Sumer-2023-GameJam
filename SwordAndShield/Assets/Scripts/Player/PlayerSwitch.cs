using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerMovement player2Movement;
    public bool player1Active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightShift)){
            switchPlayer();
        }
    }

    public void switchPlayer(){
        if(player1Active){
            PlayerMovement.enabled = false;
            player2Movement.enabled = true;
            player1Active = false;
        } else {
            PlayerMovement.enabled = true;
            player2Movement.enabled = false;
            player1Active = true;
        }
    }
}
