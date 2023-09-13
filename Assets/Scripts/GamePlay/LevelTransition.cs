using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SpaceInvaders;

public class LevelTransition : MonoBehaviour
{
    public float delay;
    public float duration;

    private float delta = 0f;
    private bool isPlaying = false;
    private TextMeshProUGUI label;
    void Start()
    {
        isPlaying = false;
        label = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (delta < delay)
            {
                label.alpha = 1f;
            }
            else if (delta < (delay + duration))
            {
                label.alpha = 1f - 1f * (delta - delay) / duration;
            }
            else
            {
                label.alpha = 0f;
                isPlaying = false;
            }

            delta += Time.deltaTime;
        }
    }

    public void StartTransition(int level)
    {
        label.text = string.Format(Utils.TranslationString("UI", "Level"), level);
        label.alpha = 0f;
        delta = 0f;
        isPlaying = true;
    }
}
