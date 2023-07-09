using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBrain : MonoBehaviour
{

    public Sprite closedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void CloseDoor()
    {
        GetComponent<SpriteRenderer>().sprite = closedSprite;
        // in the player we can make it so that it does the countdown to restart
    }
}
