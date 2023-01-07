using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{
    public delegate void GameOver();
    public event GameOver trainCrash;
    [SerializeField] UI_CoalMeter coalFill;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="train"||other.tag=="wagon")
        {
            if(other.transform.parent.gameObject.name!=transform.parent.gameObject.name)
            {
                trainCrash();
            }
        }
        else if (other.tag == "TrainStation")
        {
            UI_CoalMeter.coalRemain = UI_CoalMeter.coalMax;
            UI_CoalMeter.coalTimer = 30f;
        }
    }

}
