using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isPlaying = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        isPlaying = false;
    }

    public void PauseGame()
    {
        isPlaying = false;
    }

    public void StartGame()
    {
        isPlaying = true;
    }

    public void ResumeGame()
    {
        isPlaying = true;
    }
}
