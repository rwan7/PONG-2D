using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu Panel List")]
    public GameObject mainPanel;
    public GameObject HTPPanel;
    public GameObject timerPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        HTPPanel.SetActive(false);
        timerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void singePlayerButton()
    {
        GameData.instance.isSinglePlayer = true;
        timerPanel.SetActive(true);
    }

    public void multiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        timerPanel.SetActive(true);
    }

    public void backButton()
    {
        HTPPanel.SetActive(false);
        timerPanel.SetActive(false);
    }

    public void setTimerButton(float timer)
    {
        GameData.instance.gameTimer = timer;
        HTPPanel.SetActive(true);
        timerPanel.SetActive(false);
    }

    public void startButton()
    {
        SceneManager.LoadScene("2. Gameplay");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
