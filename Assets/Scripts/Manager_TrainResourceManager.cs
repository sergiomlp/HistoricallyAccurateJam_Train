using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_TrainResourceManager : MonoBehaviour
{
    public static Manager_TrainResourceManager instance;
    public static Manager_TrainResourceManager GetTrainResourceInstance() { return instance; }

    private string colliderEnter;
    public static int currentWagon = 3;

    private int currentIronResource;
    private int currentCottonResource;
    private int currentSpiceResource;
    private int currentLuxuryResource;

    [SerializeField] int moneymuiltiflier = 1;

    public enum FactoryResources
    {
        Iron, Cotton, Spice, Luxury, Empty
    }

    public struct wagonAndResource
    {
        public int wagonIndex;
        public FactoryResources wagonResource;

        public wagonAndResource(int i, FactoryResources r)
        {
            wagonIndex = i;
            wagonResource = r;
        }
    }
    private wagonAndResource[] wagonResourcesList = new wagonAndResource[8];

    private bool allWagonFull;
    public bool isDoubleGoods;

    public static event Action<int, FactoryResources> wagonUpdate;
    public static event Action<int> scoreMoneyUpdate;

    void Start()
    {
        instance = this;
        SetupInitialWagon();
        UI_Wagon.wagonEmpty += OnWargonRemove;
    }

    private void SetupInitialWagon()
    {
        for (int i = 0; i < 8; i++)
        {
            wagonAndResource add = new wagonAndResource(i, FactoryResources.Empty);
            wagonResourcesList[i] = add;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        colliderEnter = other.tag;
        switch (colliderEnter)
        {
            case "IronResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Iron);
                if (isDoubleGoods)
                {
                    CheckIfWagonEmptyAndFill(FactoryResources.Iron);
                }
                break;
            case "CottonResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Cotton);
                if (isDoubleGoods)
                {
                    CheckIfWagonEmptyAndFill(FactoryResources.Cotton);
                }
                break;
            case "SpiceResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Spice);
                if (isDoubleGoods)
                {
                    CheckIfWagonEmptyAndFill(FactoryResources.Spice);
                }
                break;
            case "LuxuryResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Luxury);
                if (isDoubleGoods)
                {
                    CheckIfWagonEmptyAndFill(FactoryResources.Luxury);
                }
                break;
            case "SteelFactory":
                CheckIfAbleToDeliver(other);
                break;
            case "FoodFactory":
                CheckIfAbleToDeliver(other);
                break;
            case "ClothFactory":
                CheckIfAbleToDeliver(other);
                break;
            default: break;
        }
    }

    private void CheckIfWagonEmptyAndFill(FactoryResources cargo)
    {
        for (int i = 0; i < currentWagon; ++i)
        {
            if (wagonResourcesList[i].wagonResource != FactoryResources.Empty) { allWagonFull = true; continue; }
            else
            {
                if(cargo == FactoryResources.Iron)
                {
                    currentIronResource++;
                }
                else if (cargo == FactoryResources.Cotton)
                {
                    currentCottonResource++;
                }
                else if (cargo!= FactoryResources.Spice)
                {
                    currentSpiceResource++;
                }
                else
                {
                    currentLuxuryResource++;
                }
                wagonResourcesList[i].wagonResource = cargo;
                wagonUpdate(i, cargo);
                allWagonFull= false;
                return;
            }
        }
    }

    private void CheckIfAbleToDeliver(Collider Factory)
    {
        Mechanic_FactoryRequest fQuest = Factory.transform.GetComponent<Mechanic_FactoryRequest>();
        if (!fQuest.isRequesting) return;
        else
        {
            if (fQuest.currentRequestLevel < 3)
            {
                Check1Cargo(fQuest);
            }
            else if (fQuest.currentRequestLevel < 5)
            {
                if (fQuest.secondRequestValue == 0)
                {
                    Check1Cargo(fQuest);
                }
                else
                {
                    Check2Cargo(fQuest);
                }
            }
            else
            {
                if (fQuest.secondRequestValue == 0 && fQuest.thirdRequestValue == 0)
                {
                    Check1Cargo(fQuest);
                }
                else if (fQuest.secondRequestValue == 1 && fQuest.thirdRequestValue == 0)
                {
                    Check2Cargo(fQuest);
                }
                else if (fQuest.secondRequestValue == 0 && fQuest.thirdRequestValue == 1)
                {
                    Check2Cargo2(fQuest);
                }
                else
                {
                    Check3Cargo(fQuest);
                }
            }
        }
    }

    private void Check3Cargo(Mechanic_FactoryRequest fQuest)
    {
        if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && currentIronResource >= 1 && currentSpiceResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && currentCottonResource >= 1 && currentSpiceResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && currentCottonResource >= 1 && currentIronResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource + currentLuxuryResource && currentIronResource >= 1 && currentSpiceResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(currentCottonResource, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource + currentLuxuryResource && currentIronResource >= 1 && currentCottonResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(currentSpiceResource, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource + currentLuxuryResource && currentSpiceResource >= 1 && currentCottonResource >= 1)
        {
            DeductCargo(fQuest.firstRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(currentSpiceResource, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 3);
        }
    }

    private void Check2Cargo(Mechanic_FactoryRequest fQuest)
    {
        if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(currentCottonResource, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(currentCottonResource, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.secondRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentCottonResource && fQuest.secondRequestResource == FactoryResources.Spice && fQuest.secondRequestValue <= currentSpiceResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.secondRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(currentSpiceResource, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.secondRequestResource == FactoryResources.Cotton && fQuest.secondRequestValue <= currentCottonResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource + currentLuxuryResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.secondRequestResource == FactoryResources.Iron && fQuest.secondRequestValue <= currentIronResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.secondRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(fQuest.secondRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.secondRequestValue) * moneymuiltiflier * 2);
        }
    }

    private void Check2Cargo2(Mechanic_FactoryRequest fQuest)
    {
        if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(currentCottonResource, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(currentCottonResource, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            DeductCargo(fQuest.thirdRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentCottonResource && fQuest.thirdRequestResource == FactoryResources.Spice && fQuest.thirdRequestValue <= currentSpiceResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            DeductCargo(fQuest.thirdRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(currentSpiceResource, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.thirdRequestResource == FactoryResources.Cotton && fQuest.thirdRequestValue <= currentCottonResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue - currentCottonResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource + currentLuxuryResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue - currentSpiceResource, FactoryResources.Luxury);
            DeductCargo(currentIronResource, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource && fQuest.thirdRequestResource == FactoryResources.Iron && fQuest.thirdRequestValue <= currentIronResource + currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            DeductCargo(fQuest.thirdRequestValue - currentIronResource, FactoryResources.Luxury);
            DeductCargo(fQuest.thirdRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate((fQuest.firstRequestValue + fQuest.thirdRequestValue) * moneymuiltiflier * 2);
        }
    }


    private void Check1Cargo(Mechanic_FactoryRequest fQuest)
    {
        if (fQuest.firstRequestResource == FactoryResources.Cotton && fQuest.firstRequestValue <= currentCottonResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Cotton);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate(fQuest.firstRequestValue * moneymuiltiflier);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Iron && fQuest.firstRequestValue <= currentIronResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Iron);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate(fQuest.firstRequestValue * moneymuiltiflier);
        }
        else if (fQuest.firstRequestResource == FactoryResources.Spice && fQuest.firstRequestValue <= currentSpiceResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Spice);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate(fQuest.firstRequestValue * moneymuiltiflier);
        }
        else if (fQuest.firstRequestValue <= currentLuxuryResource)
        {
            DeductCargo(fQuest.firstRequestValue, FactoryResources.Luxury);
            fQuest.SuccessfulDelivery();
            scoreMoneyUpdate(fQuest.firstRequestValue * moneymuiltiflier);
        }
    }

    private void DeductCargo(int amount, FactoryResources resource)
    {
        for (int i = 0; i < currentWagon; ++i)
        {
            if (amount == 0) { return; }
            if (wagonResourcesList[i].wagonResource != resource) continue;
            else
            {
                wagonResourcesList[i].wagonResource = FactoryResources.Empty;
                if (resource == FactoryResources.Iron) { currentIronResource -= 1; }
                else if (resource == FactoryResources.Cotton) { currentCottonResource -= 1; }
                else if (resource == FactoryResources.Spice) { currentSpiceResource -= 1; }
                else { currentLuxuryResource -= 1; }
                wagonUpdate(i, FactoryResources.Empty);
                amount -= 1;
            }
        }        
    }

    private void OnWargonRemove(int index)
    {        
        if (wagonResourcesList[index].wagonResource != FactoryResources.Empty)
        {
            if (wagonResourcesList[index].wagonResource == FactoryResources.Iron)
            {
                DeductSpecificCargo(index, FactoryResources.Iron);
            }
            else if (wagonResourcesList[index].wagonResource == FactoryResources.Cotton)
            {
                DeductSpecificCargo(index, FactoryResources.Cotton);
            }
            else if (wagonResourcesList[index].wagonResource == FactoryResources.Spice)
            {
                DeductSpecificCargo(index, FactoryResources.Spice);
            }
            else if (wagonResourcesList[index].wagonResource == FactoryResources.Luxury)
            {
                DeductSpecificCargo(index, FactoryResources.Luxury);
            }
        }
    }
    private void DeductSpecificCargo(int index, FactoryResources resource)
    {
        wagonResourcesList[index].wagonResource = FactoryResources.Empty;
        if (resource == FactoryResources.Iron) { currentIronResource -= 1; }
        else if (resource == FactoryResources.Cotton) { currentCottonResource -= 1; }
        else if (resource == FactoryResources.Spice) { currentSpiceResource -= 1; }
        else { currentLuxuryResource -= 1; }
        wagonUpdate(index, FactoryResources.Empty);
    }

    private void OnDisable()
    {
        UI_Wagon.wagonEmpty -= OnWargonRemove;
    }
}
