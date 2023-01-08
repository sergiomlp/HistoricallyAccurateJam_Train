using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Manager_TrainResourceManager;

public class UI_Wagon : MonoBehaviour
{
    [SerializeField] private GameObject[] wagonObject;
    [SerializeField] private GameObject[] wagonButtonObject;

    public static event Action<int> wagonEmpty;

    [SerializeField] private Material[] wagonMaterial;

    void Start()
    {
        Manager_TrainResourceManager.wagonUpdate += OnWagonUpdate;
        UI_UpgradeButton.OnAddCargo += OnAddCargoUI;
    }

    private void OnAddCargoUI(int index)
    {
        wagonButtonObject[index].gameObject.SetActive(true);
        wagonObject[index].gameObject.transform.GetChild(0).gameObject.SetActive(true);
        wagonObject[index].gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnWagonUpdate(int wagonIndex, Manager_TrainResourceManager.FactoryResources factoryResource)
    {
        if (factoryResource == Manager_TrainResourceManager.FactoryResources.Empty)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Iron)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = wagonMaterial[0];
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Cotton)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = wagonMaterial[1];
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else if (factoryResource == Manager_TrainResourceManager.FactoryResources.Spice)
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(true);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(false);
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = wagonMaterial[2];
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
        else
        {
            wagonButtonObject[wagonIndex].transform.GetChild(0).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(1).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(2).gameObject.SetActive(false);
            wagonButtonObject[wagonIndex].transform.GetChild(3).gameObject.SetActive(true);
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().material = wagonMaterial[3];
            wagonObject[wagonIndex].transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }

    public void WagonButton1()
    {        
        wagonEmpty(0);
    }
    public void WagonButton2()
    {
        wagonEmpty(1);
    }
    public void WagonButton3()
    {
        wagonEmpty(2);
    }
    public void WagonButton4()
    {
        wagonEmpty(3);
    }
    public void WagonButton5()
    {
        wagonEmpty(4);
    }
    public void WagonButton6()
    {
        wagonEmpty(5);
    }
    public void WagonButton7()
    {
        wagonEmpty(6);
    }
    public void WagonButton8()
    {
        wagonEmpty(7);
    }

    private void OnDisable()
    {
        Manager_TrainResourceManager.wagonUpdate -= OnWagonUpdate;
        UI_UpgradeButton.OnAddCargo -= OnAddCargoUI;
    }
}
