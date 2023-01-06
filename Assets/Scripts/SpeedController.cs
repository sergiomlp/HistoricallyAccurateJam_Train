using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    Slider slid;

    public float increment=0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
        //canvas.enabled=true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            if(child.GetComponent<locomotive>().speed!=slid.value)
            {
                if(child.GetComponent<locomotive>().speed<slid.value)
                {
                    child.GetComponent<locomotive>().speed+=increment;
                }
                else
                {
                    child.GetComponent<locomotive>().speed-=increment;
                }
            }

        }
    }

    public void function(Slider speedController,float speed,float maxSpeed)
    {
        speedController.enabled=true;
        Debug.Log(speed);
        speedController.value=speed;
        speedController.maxValue=speed;
        speedController.onValueChanged.AddListener(delegate{ ValueChangeCheck(speedController); });
    }

    public void ValueChangeCheck(Slider speedController)
    {
        slid=speedController;
        //Debug.Log(speedController.value);
        // foreach (Transform child in transform)
        // {
        //     child.GetComponent<locomotive>().speed=speedController.value;
        // }
    }
}
