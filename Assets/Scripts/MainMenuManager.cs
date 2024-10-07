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
    public GameObject ballPanel;

    [Header("Music Toggle")]
    public GameObject musicOn;
    public GameObject musicOff;

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
        ballPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void multiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        ballPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void selectFootball()
    {
        GameData.instance.selectBall = "football";
        ballPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
        timerPanel.SetActive(true);
    }

    public void selectbasketball()
    {
        GameData.instance.selectBall = "basketball";
        ballPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
        timerPanel.SetActive(true);
    }

    public void selectvolleyball()
    {
        GameData.instance.selectBall = "volleyball";
        ballPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
        timerPanel.SetActive(true);
    }

    public void musicOffButton()
    {
        SoundManager.instance.musicToggle();
        if (musicOff.activeSelf)
        {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
        }
        else
        {
            musicOff.SetActive(true);
        }
    }

    public void musicOnButton()
    {
        SoundManager.instance.musicToggle();
        if (musicOn.activeSelf)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            musicOn.SetActive(true);
        }
    }

    public void backButton()
    {
        ballPanel.SetActive(false);
        HTPPanel.SetActive(false);
        timerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void setTimerButton(float timer)
    {
        GameData.instance.gameTimer = timer;
        HTPPanel.SetActive(true);
        timerPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
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
