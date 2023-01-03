using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destinationSensor : MonoBehaviour
{
    public GameObject detectedDestination;

    // Start is called before the first frame update
    void Start()
    {
        detectedDestination=null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="traindestination")
        {
            Debug.Log("destination detected");
            detectedDestination = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="traindestination")
        {
            //detectedDestination=null;
        }
    }
}
