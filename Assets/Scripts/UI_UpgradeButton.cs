using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : MonoBehaviour
{
    [SerializeField] private Text upCoalMeterLevelText;
    [SerializeField] private Text upCoalMeterCostText;
    [SerializeField] private Text addCargoLevelText;
    [SerializeField] private Text addCargoCostText;
    [SerializeField] private Text upEngineLevelText;
    [SerializeField] private Text upEngineCostText;
    [SerializeField] private Text upBrakeLevelText;
    [SerializeField] private Text upBrakeCostText;
    [SerializeField] private Text doubleLevelText;
    [SerializeField] private Text doubleCostText;
    [SerializeField] private GameObject upgradeMenu;

    private int currentCoalMeterLevel;
    private int currentCoalMeterCost;
    private int currentcargoLevel;
    private int currentcargoCost;
    private int currentcargoIndex;
    private int currentengineLevel;
    private int currentengineCost;
    private int currentbrakeLevel;
    private int currentbrakeCost;
    private int currentdoubleLevel;
    private int currentdoubleCost;

    public static event Action<int> OnAddCargo;
    [SerializeField] private locomotive trainHead;
    [SerializeField] private UI_Money_Score_GameOverCount moneyUpdate;

    void Start()
    {
        currentCoalMeterLevel = 1;
        currentCoalMeterCost = 1;
        currentcargoLevel = 1;
        currentcargoCost = 3;
        currentcargoIndex = 3;
        currentengineLevel = 1;
        currentengineCost = 3;
        currentbrakeLevel = 1;
        currentbrakeCost = 3;
        currentdoubleLevel = 0;
        currentdoubleCost = 10;
    }

    public void ActivateUpgradeMenu()
    {
        if(upgradeMenu.activeInHierarchy)
        {
            upgradeMenu.SetActive(false);
        }
        else
        {
            upgradeMenu.SetActive(true);
        }
    }

    public void UpCoalMeter()
    {
        if (currentCoalMeterLevel < 9)
        {
            if (UI_Money_Score_GameOverCount.currentMoney >= currentCoalMeterCost)
            {
                UI_Money_Score_GameOverCount.currentMoney -= currentCoalMeterCost;
                moneyUpdate.currentMoneyText.text = UI_Money_Score_GameOverCount.currentMoney.ToString() + "K";
                UI_CoalMeter.coalMax += 1;
                currentCoalMeterLevel++;
                currentCoalMeterCost += 1;
                Manager_Audio.GetInstance().UpgradeButton.Play();
                if (currentCoalMeterLevel == 9)
                {
                    upCoalMeterLevelText.text = "Lv. max";
                    upCoalMeterCostText.text = "";
                }
                else
                {
                    upCoalMeterLevelText.text = "Lv. " + currentCoalMeterLevel.ToString();
                    upCoalMeterCostText.text = currentcargoCost.ToString() + "K";
                }
            }
        }
    }

    public void AddCargoButton()
    {
        if (currentcargoLevel < 6)
        {
            if (UI_Money_Score_GameOverCount.currentMoney >= currentcargoCost)
            {
                Manager_Audio.GetInstance().UpgradeButton.Play();
                UI_Money_Score_GameOverCount.currentMoney -= currentcargoCost;
                moneyUpdate.currentMoneyText.text = UI_Money_Score_GameOverCount.currentMoney.ToString() + "K";
                Manager_TrainResourceManager.currentWagon++;
                // UI and model active
                OnAddCargo(currentcargoIndex);
                currentcargoIndex++;
                currentcargoLevel++;
                currentcargoCost += 2;
                if (currentcargoLevel == 6)
                {
                    addCargoLevelText.text = "Lv. max";
                    addCargoCostText.text = "";
                }
                else
                {
                    addCargoLevelText.text = "Lv. " + currentcargoLevel.ToString();
                    addCargoCostText.text = currentcargoCost.ToString() + "K";
                }
            }
        }
    }
    public void UpEngineButton()
    {
        if (currentengineLevel < 5)
        {
            if (UI_Money_Score_GameOverCount.currentMoney >= currentengineCost)
            {
                Manager_Audio.GetInstance().UpgradeButton.Play();
                UI_Money_Score_GameOverCount.currentMoney -= currentengineCost;
                moneyUpdate.currentMoneyText.text = UI_Money_Score_GameOverCount.currentMoney.ToString() + "K";
                trainHead.maxspeed += 3;
                trainHead.speed = trainHead.maxspeed;

                currentengineLevel++;
                currentengineCost += 2;
                if (currentengineLevel == 5)
                {
                    upEngineLevelText.text = "Lv. max";
                    upEngineCostText.text = "";
                }
                else
                {
                    upEngineLevelText.text = "Lv. " + currentengineLevel.ToString();
                    upEngineCostText.text = currentengineCost.ToString() + "K";
                }
            }
        }
    }

    public void UpBrake()
    {
        if (currentbrakeLevel < 5)
        {
            if (UI_Money_Score_GameOverCount.currentMoney >= currentbrakeCost)
            {
                Manager_Audio.GetInstance().UpgradeButton.Play();
                UI_Money_Score_GameOverCount.currentMoney -= currentbrakeCost;
                moneyUpdate.currentMoneyText.text = UI_Money_Score_GameOverCount.currentMoney.ToString() + "K";
                UI_TrainBrakeController.brakeAccelSpeed -= 0.5f;
                currentbrakeLevel++;
                currentbrakeCost += 2;
                if (currentbrakeLevel == 5)
                {
                    upBrakeLevelText.text = "Lv. max";
                    upBrakeCostText.text = "";
                }
                else
                {
                    upBrakeLevelText.text = "Lv. " + currentbrakeLevel.ToString();
                    upBrakeCostText.text = currentbrakeCost.ToString() + "K";
                }
            }
        }
    }

    public void UpDouble()
    {
        if (currentdoubleLevel<1)
        {
            if (UI_Money_Score_GameOverCount.currentMoney >= currentdoubleCost)
            {
                Manager_Audio.GetInstance().UpgradeButton.Play();
                UI_Money_Score_GameOverCount.currentMoney -= currentdoubleCost;
                moneyUpdate.currentMoneyText.text = UI_Money_Score_GameOverCount.currentMoney.ToString() + "K";
                Manager_TrainResourceManager.GetTrainResourceInstance().isDoubleGoods = true;
                currentdoubleLevel++;
                doubleLevelText.text = "Lv. max";

                doubleCostText.text = "";
            }
        }
    }
}
