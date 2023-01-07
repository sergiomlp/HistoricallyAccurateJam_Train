using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    public int type=1;

    [SerializeField] ScoreCounter sc;


    int quantityReceived=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="train")
        {
            trm=other.GetComponent<TrainResourceManager>();
            if(type==trm.type)
            {
                Debug.Log("Quantity is equal");
                if(trm.quantity!=0)
                {
                    quantityReceived+=trm.quantity;
                    trm.quantity=0;
                    sc.score=sc.score+10;
                    Debug.Log("Factory quantity = "+quantityReceived);
                }
                else if(trm.quantity==0)
                {
                    Debug.Log("Train has nothing");
                }
            }
        }
    }*/
}
