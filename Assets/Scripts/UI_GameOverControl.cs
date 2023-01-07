using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameOverControl : MonoBehaviour
{
    [SerializeField] TrainCollision trainHead;
    [SerializeField] UI_Money_Score_GameOverCount failCounter;

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
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        trainHead.trainCrash -= OnTrainCrash;
        failCounter.fail -= OnFail;
    }
}
