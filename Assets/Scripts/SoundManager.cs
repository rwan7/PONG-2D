using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip uiButton;
    public AudioClip ballBounce;
    public AudioClip goal;
    public AudioClip gameOver;
    public bool isMute = false;

    private AudioSource audio;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameOver);
        }
        else
            instance = this;

        audio = GetComponent<AudioSource>();
    }
    
    public void musicToggle()
    {
        isMute = !isMute;
        
        if(isMute == true)
        {
            audio.volume = 0;
        }
        else
        {
            audio.volume = 1;
        }
    }
    
    public void UIClickSfx()
    {
        audio.PlayOneShot(uiButton);
    }

    public void BallBounceSfx()
    {
        audio.PlayOneShot(ballBounce);
    }

    public void GoalSfx()
    {
        audio.PlayOneShot(goal);
    }

    public void GameOverSfx()
    {
        audio.PlayOneShot(gameOver);
    }
}
