using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        GameOver();
    }

    private void GameOver()
    {
        trainHeadLoco.maxspeed = 0;
        gdpText.text = "£ " + UI_Money_Score_GameOverCount.score*1000;
        gameOverUI.SetActive(true);
    }

    private void OnDisable()
    {
        trainHead.trainCrash -= OnTrainCrash;
        failCounter.fail -= OnFail;
    }
}
