using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public bool isSinglePlayer;
    public float gameTimer;
    public string selectBall;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
            DontDestroyOnLoad(gameObject);
    }
}

