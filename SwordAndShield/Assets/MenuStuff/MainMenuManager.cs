using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button playButton,howToPlayButton,creditsButton,quitButton;
    public GameObject creditsScreen, howToPlayScreen, quitScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        //SceneManager.LoadScene();
    }

    public void HowToPlay()
    {
        transform.Find("HowToPlayPanel").gameObject.SetActive(true);
    }

    public void Credits()
    {
        transform.Find("CreditPanel").gameObject.SetActive(true);
    }

    public void QuitPanel()
    {
        transform.Find("AreYouSurePanel").gameObject.SetActive(true);
    }

    public void quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void CreditsBack()
    {
        creditsScreen.SetActive(false);
    }

    public void HowToPlayBack()
    {
        howToPlayScreen.SetActive(false);
    }

    public void QuitBack()
    {
        quitScreen.SetActive(false);
    }
}
