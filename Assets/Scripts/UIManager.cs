using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject hud;
    public GameObject resumeButton;
    public GameObject startButton;

    void Start()
    {
        resumeButton.SetActive(false);
        startButton.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.isPlaying)
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

        resumeButton.SetActive(true);
        startButton.SetActive(false);
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();

        hud.SetActive(true);
        mainMenu.SetActive(false);
    }
}
