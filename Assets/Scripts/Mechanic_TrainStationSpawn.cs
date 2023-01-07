using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_TrainStationSpawn : MonoBehaviour
{
    private bool hasSpawned;
    [SerializeField] private Transform trainhead;
    [SerializeField] private GameObject AItrain;
    void Start()
    {
        Manager_TrainResourceManager.scoreMoneyUpdate += OnMoneyUpdate;
    }

    private void OnMoneyUpdate(int obj)
    {
        if (!hasSpawned) {
            if (UI_Money_Score_GameOverCount.score >= 100)
            {
                if(trainhead.position.x <40f && trainhead.position.x>-30f && trainhead.position.z < -50f && trainhead.position.z > -60f)
                {
                    return;
                }
                else
                {
                    AItrain.SetActive(true);
                    hasSpawned = true;
                }
            }
        } 
    }
}
