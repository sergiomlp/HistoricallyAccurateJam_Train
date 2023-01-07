using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCenter : MonoBehaviour
{

    public int quantityToGive=100;
    public int type=1;


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
                if(trm.quantity==0)
                {
                    trm.quantity=100;
                }
            }
        }
    }*/
}
