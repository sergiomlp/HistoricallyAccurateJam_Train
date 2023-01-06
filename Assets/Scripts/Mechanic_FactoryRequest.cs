using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanic_FactoryRequest : MonoBehaviour
{
    private float requestRandomTimer;
    [SerializeField] private type FactoryType;
    private float deliveryTimer;
    private int deliveryCount;
    private int maxRequestLevel = 1;
    public int currentRequestLevel;
    private bool isRequesting;

    public int firstRequestValue;
    public Manager_TrainResourceManager.FactoryResources firstRequestResource;
    public int secondRequestValue;
    public Manager_TrainResourceManager.FactoryResources secondRequestResource;
    public int thirdRequestValue;
    public Manager_TrainResourceManager.FactoryResources thirdRequestResource;



    public delegate void RequestEventHandler();
    public event RequestEventHandler requestEvent;
    public delegate void RequestFailedEventHandler();
    public event RequestFailedEventHandler requestFailed;

    private enum type
    {
        Steel, Food, Cloth
    }
    

    void Start()
    {
        RandomRequestTime();
        currentRequestLevel = maxRequestLevel;
        firstRequestResource = 0;
        secondRequestValue = 0;
        thirdRequestValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRequesting) 
        { 
            requestRandomTimer -= Time.deltaTime; 
            if (requestRandomTimer < 0)
            {
                GenerateRequest();
            }
        }
        else
        {
            deliveryTimer-= Time.deltaTime;
            if(deliveryTimer <0)
            {
                requestFailed();
                isRequesting = false;
            }
        }
    }

    private void RandomRequestTime()
    {
        requestRandomTimer = Random.Range(3f, 10f);
    }

    private void GenerateRequest()
    {
        switch (FactoryType)
        {
            case type.Steel:
                int requestRandom = Random.Range(1, maxRequestLevel + 1);
                if(requestRandom < 3)
                {
                    currentRequestLevel = requestRandom;
                    firstRequestValue = Random.Range(1, 3);
                    firstRequestResource = Manager_TrainResourceManager.FactoryResources.Iron;
                }
                else if (requestRandom < 4)
                {

                }
                break;
            case type.Food:
                break;
            case type.Cloth:
                break;
            default:
                break;
        }
    }
}
