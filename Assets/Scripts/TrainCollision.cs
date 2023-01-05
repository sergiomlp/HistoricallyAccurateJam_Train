using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCollision : MonoBehaviour
{
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
        if(other.tag=="train"||other.tag=="wagon")
        {
            if(other.transform.parent.gameObject.name!=transform.parent.gameObject.name)
            {
                Debug.Log("GameOver");
            }
        }
    }

}
