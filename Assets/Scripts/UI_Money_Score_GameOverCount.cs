using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Money_Score_GameOverCount : MonoBehaviour
{    
    public static int currentMoney;
    public Text currentMoneyText;
    [SerializeField] Text incomeText;
    [SerializeField] Animation incomeAnimation;

    public static int score;
    [SerializeField] Text scoreText;

    public static int failCount;
    [SerializeField] Text failCountText;

    public delegate void GameOver();
    public event GameOver fail;

    void Start()
    {
        Manager_TrainResourceManager.scoreMoneyUpdate += OnMoneyUpdate;
        Mechanic_FactoryRequest.OnRequestFailed += OnRequestFail;
        currentMoney = 0;
        failCount = 0;
        score = 0;
    }

    private void OnRequestFail()
    {
        failCount++;
        failCountText.text = failCount + "/5";
    }

    private void OnMoneyUpdate(int value)
    {       
        incomeText.text = value.ToString() + "K";
        incomeAnimation.Play();
        currentMoney += value;
        currentMoneyText.text = currentMoney.ToString() + "K";
        Manager_Audio.GetInstance().EarnMoney.Play();
        score += value;
        scoreText.text = score.ToString();
    }

    private void OnDisable()
    {
        Manager_TrainResourceManager.scoreMoneyUpdate -= OnMoneyUpdate;
        Mechanic_FactoryRequest.OnRequestFailed -= OnRequestFail;
    }
}
