using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceInvaders;
using SimpleJson;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gamePlayObj;
    public Transform bulletContainer;
    public PlayerShip playerShip;
    public LevelTransition levelTransition;
    public bool isPlaying = false;
    public bool isGameOver = false;
    public int currentLevel = 1;
    public int life = 3;
    public int score = 0;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        isPlaying = false;
        isGameOver = false;
    }

    public void PauseGame()
    {
        isPlaying = false;
    }

    public void StartGame()
    {
        Utils.DestroyChildren(bulletContainer);
        isPlaying = true;
        isGameOver = false;
        currentLevel = 1;
        score = 0;
        life = 3;
        gamePlayObj.SetActive(true);
        playerShip.InitShip();
        StartLevel();
        UIManager.instance.UpdateScore();
        UIManager.instance.UpdateLife();
    }

    public void StartLevel()
    {
        levelTransition.StartTransition(currentLevel);
        EnemyManager.instance.CreateEnemies(LevelManager.GetLevelInfo(currentLevel));
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }

    public void AddScore(int point)
    {
        score += point;
        UIManager.instance.UpdateScore();
    }

    public void ReduceLife()
    {
        life--;
        if (life == 0)
        {
            isGameOver = true;
            UIManager.instance.ShowGameOver();
            if (score > 0)
            {
                StoreScore(score);
            }
        }
        UIManager.instance.UpdateLife();
    }

    public void GameOver()
    {
        isGameOver = true;
        UIManager.instance.ShowGameOver();
        playerShip.GameOver();
        if (score > 0)
        {
            StoreScore(score);
        }
    }

    public void ExitGame()
    {
        gamePlayObj.SetActive(false);
        isPlaying = false;
    }

    public void NextLevel()
    {
        currentLevel++;
        StopCoroutine("StartNextLevel");
        StartCoroutine("StartNextLevel");
    }

    IEnumerator StartNextLevel()
    {
        yield return new WaitForSeconds(1f);
        StartLevel();
    }

    public void StoreScore(int newScore)
    {
        List<int> scores = new List<int>();
        if (PlayerPrefs.HasKey("scores"))
        {
            List<object> json = SimpleJson.SimpleJson.DeserializeObject(PlayerPrefs.GetString("scores")) as List<object>;
            foreach (object each in json)
            {
                scores.Add(Convert.ToInt32(each));
            }
        }
        scores.Add(newScore);
        scores.Sort();
        scores.Reverse();
        if (scores.Count > 7)
        {
            scores.RemoveAt(scores.Count - 1);
        }
        PlayerPrefs.SetString("scores", SimpleJson.SimpleJson.SerializeObject(scores));
    }

    public List<int> GetStoredScores()
    {
        List<int> scores = new List<int>();
        if (PlayerPrefs.HasKey("scores"))
        {
            List<object> json = SimpleJson.SimpleJson.DeserializeObject(PlayerPrefs.GetString("scores")) as List<object>;
            foreach (object each in json)
            {
                scores.Add(Convert.ToInt32(each));
            }
        }
        scores.Sort();
        scores.Reverse();

        return scores;
    }
}
