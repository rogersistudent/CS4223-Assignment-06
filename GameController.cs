using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System;

public class GameController : MonoBehaviour
{
    public float coinTime = 5f;
    public CoinSpawner coinSpawner;
    public float enemyTime = 10f;
    public EnemySpawner enemySpawner;
    public GameObject youDied;
    public GameObject pause;
    int lives;
    public int score = 0;
    public Text scoreboard;
    public Text live;

    private void Start()
    {
        lives = PlayerPrefs.GetInt("lives");
        Time.timeScale = 1f;
        Invoke("spawnCoin", coinTime);
        InvokeRepeating("spawnEnemy", enemyTime, enemyTime);
    }

    void Update()
    {
        live.text = "LIVES: " + lives;
        scoreboard.text = "SCORE: " + score;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void Dead()
    {
        int tempscore = PlayerPrefs.GetInt("score");
        if(score > tempscore)
        {
            PlayerPrefs.SetInt("score", score);
        }
        if (Time.timeScale == 1) lives--; 
        if(lives == 0)
        {
            AddNewScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        PlayerPrefs.SetInt("lives", lives);
        youDied.SetActive(true);
        Time.timeScale = .5f;
        Invoke("Restart", 2f);
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void PauseGame()
    {
        Time.timeScale = 0;
        pause.SetActive(true);
    }
    void ResumeGame()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    void spawnCoin()
    {
        coinSpawner.SpawnCoin();
    }
    void spawnEnemy()
    {
        enemySpawner.SpawnEnemy();
    }
    public int num_scores = 10;

    public void AddNewScore()
    {
        string path = "Assets/scores.txt";
        string line;
        string[] fields;
        int scores_written = 0;
        string newName = "don't forget to input";
        string newScore = "999";
        bool newScoreWritten = false;
        string[] writeNames = new string[10];
        string[] writeScores = new string[10];

        newName = PlayerPrefs.GetString("username");
        newScore = PlayerPrefs.GetInt("score").ToString();

        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();
            fields = line.Split(',');
            if (!newScoreWritten && scores_written < num_scores) // if new score has not been written yet
            {
                //check if we need to write new higher score first
                if (Convert.ToInt32(newScore) > Convert.ToInt32(fields[1]))
                {
                    writeNames[scores_written] = newName;
                    writeScores[scores_written] = newScore;
                    newScoreWritten = true;
                    scores_written += 1;
                }

            }
            if (scores_written < num_scores) // we have not written enough lines yet
            {
                writeNames[scores_written] = fields[0];
                writeScores[scores_written] = fields[1];
                scores_written += 1;
            }
        }
        reader.Close();

        // now we have parallel arrays with names and scores to write
        StreamWriter writer = new StreamWriter(path);

        for (int x = 0; x < scores_written; x++)
        {
            writer.WriteLine(writeNames[x] + ',' + writeScores[x]);
        }
        writer.Close();

        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("scores");

    }
}
