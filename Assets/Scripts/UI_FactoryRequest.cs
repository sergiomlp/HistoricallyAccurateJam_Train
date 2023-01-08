using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_FactoryRequest : MonoBehaviour
{
    [SerializeField] private Image requestUIPrefab;
    private Image uiUse;
    [SerializeField] private Vector3 offset = new Vector3(0, 15f, 0);
    [SerializeField] private Canvas HUDcanvas;

    [SerializeField] private bool IsRequesting;

    private float timer;
    private Image timerImage;
    private GameObject request1;
    private Image request1Sprite;
    private Text request1Value;
    private GameObject request2;
    private Image request2Sprite;
    private Text request2Value;
    private GameObject request3;
    private Image request3Sprite;
    private Text request3Value;
    private RectTransform backgroundWidth;

    private Mechanic_FactoryRequest factoryRequest;
    [SerializeField] private Sprite[] factoryResource;

    void Start()
    {
        uiUse = Instantiate(requestUIPrefab, HUDcanvas.transform);
        uiUse.transform.SetSiblingIndex(0);
        uiUse.gameObject.SetActive(false);
        GetValue();
        factoryRequest = transform.GetComponent<Mechanic_FactoryRequest>();
        factoryRequest.requestEvent += HandleRequestEvent;
        factoryRequest.requestReset += HandleRequestResetEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRequesting)
        {
            uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            timer = factoryRequest.deliveryTimer / factoryRequest.maxDeliveryTimer;
            timerImage.fillAmount = timer;
        }        
    }

    private void GetValue()
    {
        timerImage = uiUse.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        request1 = uiUse.transform.GetChild(1).gameObject;
        request1Sprite = uiUse.transform.GetChild(1).GetComponent<Image>();
        request1Value = uiUse.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        request2 = uiUse.transform.GetChild(2).gameObject;
        request2Sprite = uiUse.transform.GetChild(2).GetComponent<Image>();
        request2Value = uiUse.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        request3 = uiUse.transform.GetChild(3).gameObject;
        request3Sprite = uiUse.transform.GetChild(2).GetComponent<Image>();
        request3Value = uiUse.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        backgroundWidth = uiUse.GetComponent<RectTransform>();
    }

    private void HandleRequestEvent(int level, int fN, Manager_TrainResourceManager.FactoryResources fR, int sN, Manager_TrainResourceManager.FactoryResources sR, int tN, Manager_TrainResourceManager.FactoryResources tR)
    {        
        if (level < 3)
        {
            Set1Sprite(fN, fR);
        }
        else if (level < 5)
        {
            if (sN == 0)
            {
                Set1Sprite(fN, fR);
            }
            else
            {
                Set2Sprite(fN, fR, sN);
            }
        }
        else
        {
            if (sN == 0 && tN == 0) 
            {
            Set1Sprite(fN, fR);
            }
            else if (sN == 1 && tN == 0)
            {
                Set2Sprite(fN, fR,sN);
            }
            else if (sN == 0 && tN == 1)
            {
                Set2Sprite2(fN, fR, tN);
            }
            else
            {
                Set3Sprite(fN, fR, sN, tN);
            }
        }
        uiUse.gameObject.SetActive(true);
        IsRequesting = true;
    }

    private void Set1Sprite(int fN, Manager_TrainResourceManager.FactoryResources fR)
    {
        request1Value.text = fN.ToString();
        if (fR == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            request1Sprite.sprite = factoryResource[0];
            Debug.Log(request1Sprite);
        }
        else if (fR == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            request1Sprite.sprite = factoryResource[1];
        }
        else
        {
            request1Sprite.sprite = factoryResource[2];
        }
        request1.SetActive(true);
        request2.SetActive(false);
        request3.SetActive(false);
        backgroundWidth.sizeDelta = new Vector2( 150f, 80f);
    }

    private void Set2Sprite(int fN, Manager_TrainResourceManager.FactoryResources fR, int sN)
    {
        request1Value.text = fN.ToString();
        request2Value.text = sN.ToString();
        if (fR == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            request1Sprite.sprite = factoryResource[0];
            request2Sprite.sprite = factoryResource[1];
        }
        else if (fR == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            request1Sprite.sprite = factoryResource[1];
            request2Sprite.sprite = factoryResource[2];
        }
        else
        {
            request1Sprite.sprite = factoryResource[2];
            request2Sprite.sprite = factoryResource[0];
        }
        request1.SetActive(true);
        request2.SetActive(true);
        request3.SetActive(false);
        backgroundWidth.sizeDelta = new Vector2(230f, 80f);
    }

    private void Set2Sprite2(int fN, Manager_TrainResourceManager.FactoryResources fR, int sN)
    {
        request1Value.text = fN.ToString();
        request2Value.text = sN.ToString();
        if (fR == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            request1Sprite.sprite = factoryResource[0];
            request2Sprite.sprite = factoryResource[2];
        }
        else if (fR == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            request1Sprite.sprite = factoryResource[1];
            request2Sprite.sprite = factoryResource[0];
        }
        else
        {
            request1Sprite.sprite = factoryResource[2];
            request2Sprite.sprite = factoryResource[1];
        }
        request1.SetActive(true);
        request2.SetActive(true);
        request3.SetActive(false);
        backgroundWidth.sizeDelta = new Vector2(230f, 80f);
    }

    private void Set3Sprite(int fN, Manager_TrainResourceManager.FactoryResources fR, int sN, int tN)
    {
        request1Value.text = fN.ToString();
        request2Value.text = sN.ToString();
        request3Value.text = tN.ToString();
        if (fR == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            request1Sprite.sprite = factoryResource[0];
            request2Sprite.sprite = factoryResource[1];
            request3Sprite.sprite = factoryResource[2];
        }
        else if (fR == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            request1Sprite.sprite = factoryResource[1];
            request2Sprite.sprite = factoryResource[2];
            request3Sprite.sprite = factoryResource[0];
        }
        else
        {
            request1Sprite.sprite = factoryResource[2];
            request2Sprite.sprite = factoryResource[0];
            request3Sprite.sprite = factoryResource[1];
        }
        request1.SetActive(true);
        request2.SetActive(true);
        request3.SetActive(true);
        backgroundWidth.sizeDelta = new Vector2(305f, 80f);
    }

    private void HandleRequestResetEvent()
    {
        uiUse.gameObject.SetActive(false);
        IsRequesting = false;
    }

    private void OnDisable()
    {
        factoryRequest.requestEvent -= HandleRequestEvent;
        factoryRequest.requestReset -= HandleRequestResetEvent;
    }    
}
