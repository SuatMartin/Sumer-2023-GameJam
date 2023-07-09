using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("player"))
        {
            if (collision.gameObject.GetComponent<playerGame>().keyCount > 0)
            {
                Destroy(this);
            }
        }*/
    }
}
