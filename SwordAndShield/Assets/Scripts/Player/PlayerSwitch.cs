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

    Vector3 spawnPoint;
    [SerializeField] private GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.enabled = true;
        player2Movement.enabled = false;
        spawnPoint = new Vector3(player1.position.x, player1.position.y, player1.position.z);
        player1Pos = new Vector3(player1.position.x, player1.position.y, player1.position.z);
        player2Pos = new Vector3(player2.position.x, player2.position.y, player2.position.z);
        players[1].SetActive(false);
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
            spawnPoint = player1.position;
            player2.position = spawnPoint;
            player1.position = player2Pos;
            players[1].SetActive(true);
            players[0].SetActive(false);
            player1Active = false;
        } else {
            PlayerMovement.enabled = true;
            player2Movement.enabled = false;
            spawnPoint = player2.position;
            player1.position = spawnPoint;
            player2.position = player1Pos;
            players[1].SetActive(false);
            players[0].SetActive(true);
            player1Active = true;
        }
    }
}
