using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locomotive : MonoBehaviour
{

    public float speed=10;
    private Rigidbody rb;
    //public GameObject destination;

    public float turningSpeed = 1f;

    public GameObject destinationSensor;

    GameObject nextDestination;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        nextDestination=destinationSensor.GetComponent<destinationSensor>().detectedDestination;

        if(nextDestination!=null)
        {
            Vector3 lookPos = nextDestination.transform.position - transform.position;
            lookPos.y=0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation=Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*turningSpeed);


            rb.velocity=transform.forward*speed;
        }

        
    }
}
