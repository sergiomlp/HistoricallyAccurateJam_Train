using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCenter : MonoBehaviour
{

    public int quantityToGive=1;
    public int ResourceCenterType=1;

    //1 for cotton
    //2 for spice
    //3 for iron

    TrainResourceManager trm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="train")
        {
            trm=other.GetComponent<TrainResourceManager>();
            switch(ResourceCenterType)
            {
                case 1:
                    if(trm.cotton>=0&&trm.quantity<=3)
                    {
                        trm.cotton+=1;
                        trm.quantity++;
                    }
                break;

                case 2:
                    if(trm.spice>=0&&trm.quantity<=3)
                    {
                        trm.spice+=1;
                        trm.quantity++;
                    }
                break;

                case 3:
                    if(trm.iron>=0&&trm.quantity<=3)
                    {
                        trm.iron+=1;
                        trm.quantity++;
                    }
                break;
            }
        }
    }
}
