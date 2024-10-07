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
    
    //Deklarasi tipe data dan variabel
    [Header("Game Settings")]
    public int player1Score;
    public int player2Score;
    public float timer;
    public bool isOver;
    public bool goldenGoal;
    public bool isSpawnPowerUp;
    public GameObject ballSpawned;

    [Header("Prefab")]
    public GameObject footballPrefab;
    public GameObject basketballPrefab;
    public GameObject volleyballPrefab;
    private GameObject selectedBallPrefab;
    public GameObject[] powerUp;

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
        goldenGoalUI.SetActive(false);

        timer = GameData.instance.gameTimer;
        isOver = false;
        goldenGoal = false;

        spawnBall();

        switch (GameData.instance.selectBall)
        {
            case "football":
                selectedBallPrefab = footballPrefab;
                break;
            case "basketball":
                selectedBallPrefab = basketballPrefab;
                break;
            case "volleyball":
                selectedBallPrefab = volleyballPrefab;
                break;
            default:
                selectedBallPrefab = footballPrefab;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        player1ScoreText.text = player1Score.ToString();
        player2ScoreText.text = player2Score.ToString();
    
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (seconds % 20 == 0 && !isSpawnPowerUp)
            {
                StartCoroutine("SpawnPowerUp");
            }
        }

        if (timer <= 0f && !isOver)
        {
            timerText.text = "00:00";
            if (player1Score == player2Score)
            {
                if (!goldenGoal)
                {
                    goldenGoal = true;

                    Ball[] ball = FindObjectsOfType<Ball>();
                    for (int i = 0; i < ball.Length; i++)
                    {
                        Destroy(ball[i].gameObject);
                    }

                    goldenGoalUI.SetActive(true);

                    spawnBall();
                }
            }
            else
            {
                GameOver();
            }
        }
    } 

    public IEnumerator SpawnPowerUp()
    {
        isSpawnPowerUp = true;
        Debug.Log("Power Up");
        int rand = Random.Range(0, powerUp.Length);
        Instantiate(powerUp[rand], new Vector3(Random.Range(-3.2f, 3.2f), Random.Range(-2.35f, 2.35f), 0), Quaternion.identity);
        yield return new WaitForSeconds(1);
        isSpawnPowerUp = false;
    }

    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void resumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        SoundManager.instance.UIClickSfx();
    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("1. Main Menu");
        SoundManager.instance.UIClickSfx();
    }

    public void restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("2. Gameplay");
        SoundManager.instance.UIClickSfx();
    }
           
    public void spawnBall()
    {
        Debug.Log("Ball Spawned");
        StartCoroutine("DelaySpawn");
    }

    public void GameOver()
    {
        SoundManager.instance.GameOverSfx();
        isOver = true;
        Debug.Log("Game Over");
        Time.timeScale = 0;

        gameOverPanel.SetActive(true);
        if (!GameData.instance.isSinglePlayer)
        {
            if (player1Score > player2Score)
            {
                player1WinUI.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                player2WinUI.SetActive(true);
            }
        }
        else
        {
            if (player1Score > player2Score)
            {
                youWin.SetActive(true);
            }
            if (player1Score < player2Score)
            {
                youLose.SetActive(true);
            }
        }
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(3);
        if (ballSpawned == null)
        {
            ballSpawned = Instantiate(selectedBallPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
