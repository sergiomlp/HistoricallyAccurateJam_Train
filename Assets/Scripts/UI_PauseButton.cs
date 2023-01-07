using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseButton : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] GameObject pauseScreen;

    public void pauseClicked()
    {
        if (!isPaused)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false; ;
        }
    }
}
