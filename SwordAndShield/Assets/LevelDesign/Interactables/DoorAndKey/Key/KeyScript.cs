using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    //public GameObject player;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Hi");
            //var player = collision.gameObject.GetComponent<playerScript>().AddToKeyCount;
            Destroy(this);
        }
        else
        {
            print("Bye");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Hi");
            //var player = collision.gameObject.GetComponent<playerScript>().AddToKeyCount;
            Destroy(gameObject);
        }
        else
        {
            print("Bye");
        }
    }
}
