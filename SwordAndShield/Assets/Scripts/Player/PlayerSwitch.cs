using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public PlayerMovement player2Movement;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    public bool player1Active = true;
    Vector3 player1Pos;
    Vector3 player2Pos;
    // Start is called before the first frame update
    void Start()
    {
        player1Pos = new Vector3(player1.position.x, player1.position.y, player1.position.z);
        player2Pos = new Vector3(player2.position.x, player2.position.y, player2.position.z);
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
            player1Pos = player1.position;
            player2.position = player1Pos;
            player1.position = player2Pos;
            player1Active = false;
        } else {
            PlayerMovement.enabled = true;
            player2Movement.enabled = false;
            player2Pos = player2.position;
            player2.position = player1Pos;
            player1.position = player2Pos;
            player1Active = true;
        }
    }
}
