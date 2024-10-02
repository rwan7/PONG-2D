using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Prefab")]
    public GameObject ballPrefab;

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject gameOverPanel;

    [Header("Ingame UI")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public GameObject goldenGoalUI;

    [Header("Game Over UI")]
    public GameObject player1WinUI;
    public GameObject player2WinUI;
        public GameObject youWin;
        public GameObject youLose;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        player1WinUI.SetActive(false);
        player2WinUI.SetActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);

        spawnBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1. Main Menu");
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("2. Gameplay");
    }

    public void spawnBall()
    {
        Debug.Log("Ball Spawned");
        StartCoroutine("DelaySpawn");
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
    }
}
