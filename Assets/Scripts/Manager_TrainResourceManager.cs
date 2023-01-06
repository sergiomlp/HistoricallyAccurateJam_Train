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
    private wagonAndResource[] wagonResourcesList;

    [SerializeField] private Transform[] wagonList;
    private bool allWagonFull;

    public static event Action<int, FactoryResources> wagonUpdate;
    public static event Action<int> scoreMoneyUpdate;

    void Start()
    {
        instance = this;
        SetupInitialWagon();
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
                break;
            case "CottonResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Cotton);
                break;
            case "SpiceResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Spice);
                break;
            case "LuxuryResource":
                CheckIfWagonEmptyAndFill(FactoryResources.Luxury);
                break;
            case "SteelFactory":
                CheckIfAbleToDeliver();
                break;
            case "FoodFactory":
                CheckIfAbleToDeliver();
                break;
            case "ClothFactory":
                CheckIfAbleToDeliver();
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
                wagonUpdate(i, cargo);
                allWagonFull= false;
                return;
            }
        }
    }

    private void CheckIfAbleToDeliver()
    {

    }
}
