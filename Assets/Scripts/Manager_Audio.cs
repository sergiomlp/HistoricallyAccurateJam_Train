using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Audio : MonoBehaviour
{
    public static Manager_Audio instance;
    public static Manager_Audio GetInstance() { return instance; }

    public AudioSource EarnMoney;
    public AudioSource Brake;
    public AudioSource UpgradeButton;
    public AudioSource Crash;
    public AudioSource trainHorn;
    public AudioSource ResocureGet;
    public AudioSource RequestSound;

    private void Start()
    {
        instance = this;
        trainHorn.Play();
    }
}
