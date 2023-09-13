using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SpaceInvaders;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject mainMenu;
    public GameObject hud;
    public GameObject gameover;
    public GameObject scoreWin;
    public GameObject resumeButton;
    public GameObject startButton;
    public TextMeshProUGUI scoreLabel;
    public Transform lifeContain;
    public Transform scoreContain;
    public GameObject scorePrefab;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        resumeButton.SetActive(false);
        startButton.SetActive(true);
        scoreWin.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.isPlaying && !GameManager.instance.isGameOver)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();

        hud.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void StartGame()
    {
        GameManager.instance.StartGame();

        hud.SetActive(true);
        mainMenu.SetActive(false);
        gameover.SetActive(false);

        resumeButton.SetActive(true);
        startButton.SetActive(false);
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();

        hud.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void UpdateScore()
    {
        scoreLabel.text = Utils.FormatNumber(GameManager.instance.score);
    }

    public void UpdateLife()
    {
        for (int i = 0; i < GameManager.instance.life; i++)
        {
            lifeContain.GetChild(i).gameObject.SetActive(true);
        }

        for (int i = GameManager.instance.life; i < 3; i++)
        {
            lifeContain.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void QuitGame()
    {
        GameManager.instance.ExitGame();
        mainMenu.SetActive(true);
        hud.SetActive(false);
        gameover.SetActive(false);
        scoreWin.SetActive(false);

        resumeButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameover.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        hud.SetActive(false);
        gameover.SetActive(false);
        scoreWin.SetActive(false);

        resumeButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void ShowScoreUI()
    {
        mainMenu.SetActive(false);
        hud.SetActive(false);
        gameover.SetActive(false);
        scoreWin.SetActive(true);

        resumeButton.SetActive(false);
        startButton.SetActive(true);

        Utils.DestroyChildren(scoreContain);

        List<int> scores = GameManager.instance.GetStoredScores();
        foreach (int score in scores)
        {
            GameObject obj = Instantiate(scorePrefab, scoreContain);
            TextMeshProUGUI label = obj.GetComponent<TextMeshProUGUI>();
            label.text = Utils.FormatNumber(score);
        }
    }
}
