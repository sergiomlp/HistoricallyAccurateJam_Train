using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    Slider slid;

    // Start is called before the first frame update
    void Start()
    {
        
        //canvas.enabled=true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void function(Slider speedController,float speed)
    {
        speedController.enabled=true;
        Debug.Log(speed);
        speedController.value=speed;
        speedController.onValueChanged.AddListener(delegate{ ValueChangeCheck(speedController); });
    }

    public void ValueChangeCheck(Slider speedController)
    {
        //Debug.Log(speedController.value);
        foreach (Transform child in transform)
        {
            child.GetComponent<locomotive>().speed=speedController.value;
        }
    }
}
