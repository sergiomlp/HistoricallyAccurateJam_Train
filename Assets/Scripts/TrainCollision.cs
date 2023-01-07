using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{
    public delegate void GameOver();
    public event GameOver trainCrash;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="train"||other.tag=="wagon")
        {
            if(other.transform.parent.gameObject.name!=transform.parent.gameObject.name)
            {
                trainCrash();
            }
        }
    }

}
