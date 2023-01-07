using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Wagon : MonoBehaviour
{
    [SerializeField] private GameObject[] wagonObject;
    [SerializeField] private GameObject[] wagonButtonObject;
    // Start is called before the first frame update
    void Start()
    {
        Manager_TrainResourceManager.wagonUpdate += OnWagonUpdate;
    }

    private void OnWagonUpdate(int wagonIndex, Manager_TrainResourceManager.FactoryResources factoryResource)
    {
        if (factoryResource == Manager_TrainResourceManager.FactoryResources.Empty)
        {
            foreach (GameObject spite in wagonButtonObject[0].transform)
            {
                spite.SetActive(false);
            }
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Spice)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        Manager_TrainResourceManager.wagonUpdate -= OnWagonUpdate;
    }
}
