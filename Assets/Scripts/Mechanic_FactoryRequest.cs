using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using static Mechanic_FactoryRequest;

public class Mechanic_FactoryRequest : MonoBehaviour
{
    public Mechanic_FactoryRequest instance;
    public Mechanic_FactoryRequest getInstance() { return instance; }

    private float requestRandomTimer;
    [SerializeField] private type FactoryType;
    public float deliveryTimer;
    public float maxDeliveryTimer;
    private int deliveryCount;
    private int maxRequestLevel = 1;
    public int currentRequestLevel;
    public bool isRequesting;

    public int firstRequestValue;
    public Manager_TrainResourceManager.FactoryResources firstRequestResource;
    public int secondRequestValue;
    public Manager_TrainResourceManager.FactoryResources secondRequestResource;
    public int thirdRequestValue;
    public Manager_TrainResourceManager.FactoryResources thirdRequestResource;

    public delegate void RequestEventHandler(int level, int fN, Manager_TrainResourceManager.FactoryResources fR, int sN, Manager_TrainResourceManager.FactoryResources sR, int tN, Manager_TrainResourceManager.FactoryResources tR);
    public event RequestEventHandler requestEvent;
    public delegate void RequestFailedEventHandler();
    public event RequestFailedEventHandler requestReset;
    public delegate void RequestFailed();
    public static event RequestFailed OnRequestFailed;

    private enum type
    {
        Steel, Food, Cloth
    }
    

    void Start()
    {
        instance = this;
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
                GenerateRequest(FactoryType);
            }
        }
        else
        {
            deliveryTimer-= Time.deltaTime;
            if(deliveryTimer <0)
            {
                // add static fail count event
                requestReset(); 
                OnRequestFailed();
                isRequesting = false;
            }
        }
    }

    private void RandomRequestTime()
    {
        requestRandomTimer = Random.Range(3f, 10f);
    }

    private void GenerateRequest(type FactoryType)
    {
        int requestRandom = Random.Range(1, maxRequestLevel + 1);

        if (FactoryType == type.Steel) { firstRequestResource = Manager_TrainResourceManager.FactoryResources.Iron; secondRequestResource = Manager_TrainResourceManager.FactoryResources.Cotton; thirdRequestResource = Manager_TrainResourceManager.FactoryResources.Spice; }
        if (FactoryType == type.Food) { firstRequestResource = Manager_TrainResourceManager.FactoryResources.Spice; secondRequestResource = Manager_TrainResourceManager.FactoryResources.Iron; thirdRequestResource = Manager_TrainResourceManager.FactoryResources.Cotton; }
        if (FactoryType == type.Cloth) { firstRequestResource = Manager_TrainResourceManager.FactoryResources.Cotton; secondRequestResource = Manager_TrainResourceManager.FactoryResources.Spice; thirdRequestResource = Manager_TrainResourceManager.FactoryResources.Iron; }

        if (requestRandom < 2)
        {
            currentRequestLevel = requestRandom;
            firstRequestValue = Random.Range(1, 3);
            deliveryTimer = 45f;
            maxDeliveryTimer = 45f;
            requestEvent(currentRequestLevel, firstRequestValue, firstRequestResource, 0, secondRequestResource, 0, thirdRequestResource);
            isRequesting = true;
            RandomRequestTime();
        }
        else if (requestRandom < 3)
        {
            currentRequestLevel = requestRandom;
            firstRequestValue = Random.Range(1, 3);
            deliveryTimer = 30f;
            maxDeliveryTimer = 30f;
            requestEvent(currentRequestLevel, firstRequestValue, firstRequestResource, 0, secondRequestResource, 0, thirdRequestResource);
            isRequesting = true;
            RandomRequestTime();
        }
        else if (requestRandom < 4)
        {
            currentRequestLevel = requestRandom;
            firstRequestValue = Random.Range(1, 3);
            secondRequestValue = Random.Range(1, 2);
            deliveryTimer = 30f;
            maxDeliveryTimer = 30f;
            requestEvent(currentRequestLevel, firstRequestValue, firstRequestResource, secondRequestValue, secondRequestResource, 0, thirdRequestResource);
            isRequesting = true;
            RandomRequestTime();
        }
        else if (requestRandom < 5)
        {
            currentRequestLevel = requestRandom;
            firstRequestValue = Random.Range(1, 4);
            secondRequestValue = Random.Range(0, 2);
            deliveryTimer = 30f;
            maxDeliveryTimer = 30f;
            requestEvent(currentRequestLevel, firstRequestValue, firstRequestResource, secondRequestValue, secondRequestResource, 0, thirdRequestResource);
            isRequesting = true;
            RandomRequestTime();
        }
        else
        {
            currentRequestLevel = requestRandom;
            firstRequestValue = Random.Range(1, 5);
            secondRequestValue = Random.Range(0, 2);
            if (firstRequestValue + secondRequestValue == 5) { thirdRequestValue = 0; }
            else
            {
                thirdRequestValue = Random.Range(0, 2);
            }
            deliveryTimer = 30f;
            maxDeliveryTimer = 30f;
            requestEvent(currentRequestLevel, firstRequestValue, firstRequestResource, secondRequestValue, secondRequestResource, thirdRequestValue, thirdRequestResource);
            isRequesting = true;
            RandomRequestTime();
        }
    }

    public void SuccessfulDelivery()
    {
        deliveryCount++;
        if (deliveryCount == 2) 
        {
            maxRequestLevel = 2;
        }
        if ( deliveryCount == 4) 
        {
        maxRequestLevel = 3;
        }
        if (deliveryCount == 6)
        {
            maxRequestLevel = 4;
        }
        if (deliveryCount == 8)
        {
            maxRequestLevel = 5;
        }
        requestReset();
        isRequesting = false;
    }
}
