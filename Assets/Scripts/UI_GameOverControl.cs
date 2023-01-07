using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOverControl : MonoBehaviour
{
    [SerializeField] TrainCollision trainHead;
    [SerializeField] locomotive trainHeadLoco;
    [SerializeField] UI_Money_Score_GameOverCount failCounter;
    [SerializeField] GameObject gameOverUI;

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
        gameOverUI.SetActive(true);
    }

    private void OnDisable()
    {
        trainHead.trainCrash -= OnTrainCrash;
        failCounter.fail -= OnFail;
    }
}
