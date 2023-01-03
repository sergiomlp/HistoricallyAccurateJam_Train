using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wagon : MonoBehaviour
{
    public float speed=10;
    private Rigidbody rb;

    public GameObject toFollow;

    public float turningSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookPos = toFollow.transform.position - transform.position;
        lookPos.y=0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation=Quaternion.Slerp(transform.rotation,rotation,Time.deltaTime*turningSpeed);
        
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,toFollow.transform.position,step);

        if(Vector3.Distance(transform.position,toFollow.transform.position) < 0.0001f)
        {
            toFollow.transform.position *= -1.0f;
        }




        // rb.velocity=transform.forward*speed;
    }
}
