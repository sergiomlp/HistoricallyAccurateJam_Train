using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_GameOverControl : MonoBehaviour
{
    [SerializeField] TrainCollision trainHead;
    [SerializeField] locomotive trainHeadLoco;
    [SerializeField] UI_Money_Score_GameOverCount failCounter;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Text gdpText;

    void Start()
    {
        trainHead.trainCrash += OnTrainCrash;
        failCounter.fail += OnFail;
    }

    private void OnFail()
    {
        if(UI_Money_Score_GameOverCount.failCount == 5)
        {
            GameOver();
        }
    }

    private void OnTrainCrash()
    {
        Manager_Audio.GetInstance().Crash.Play();
        GameOver();
    }

    private void GameOver()
    {
        trainHeadLoco.maxspeed = 0;
        gdpText.text = "£ " + UI_Money_Score_GameOverCount.score*1000;
        gameOverUI.SetActive(true);
    }

    public void RetryBtn()
    {
        SceneManager.LoadScene(1);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDisable()
    {
        trainHead.trainCrash -= OnTrainCrash;
        failCounter.fail -= OnFail;
    }
}
