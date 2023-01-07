using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
=======
using static Manager_TrainResourceManager;
using static UI_UpgradeButton;
>>>>>>> Stashed changes

public class UI_Wagon : MonoBehaviour
{
    [SerializeField] private GameObject[] wagonObject;
    [SerializeField] private GameObject[] wagonButtonObject;
    // Start is called before the first frame update
    void Start()
    {
        Manager_TrainResourceManager.wagonUpdate += OnWagonUpdate;
        UI_UpgradeButton.OnAddCargo += OnAddCargoUI;
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

<<<<<<< Updated upstream
=======
    private void OnAddCargoUI(int index)
    {
        wagonButtonObject[index].gameObject.SetActive(true);
        wagonObject[index].transform.GetChild(0).gameObject.SetActive(true);
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

>>>>>>> Stashed changes
    private void OnDisable()
    {
        Manager_TrainResourceManager.wagonUpdate -= OnWagonUpdate;
    }
}
