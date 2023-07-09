using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {
        pauseScreen = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
    }

    public void Quit()
    {
        //SceneManager.LoadScene();
    }
}
