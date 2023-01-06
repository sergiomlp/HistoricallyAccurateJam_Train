using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locomotive : MonoBehaviour
{

    public float speed=1;
    public float maxSpeed=4;
    private Rigidbody rb;
    //public GameObject destination;

    public float coalCountDown=60.0f;
    public bool canTakeCoal=true;

    public int coalCount=3;

    public float turningSpeed = 1f;

    public GameObject destinationSensor;

    public float currentTimer=5.0f;

    GameObject nextDestination;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        
        
    }

    void OnAwake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
        }
        else
        {
            if(coalCount--<0)
            {
                speed=0.0f;
            }
            coalCount--;
            currentTimer=20.0f;
        }

        if(coalCountDown>0&&canTakeCoal==false)
        {
            coalCountDown-=Time.deltaTime;
        }
        else
        {
            canTakeCoal=true;
        }

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
