using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JunctionController : MonoBehaviour
{
    public GameObject[] wpA;
    public GameObject[] wpB;

    public int currentActive=0;

    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Junction Spawned");
        slider=GameObject.FindWithTag("Slider").GetComponent<Slider>();
        EnableIntersection(0);
        currentActive=0;
        slider.onValueChanged.AddListener(delegate{ ValueChangeCheck(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableIntersection(int part)
    {
        if(part==0)
        {
            foreach(GameObject point in wpA)
            {
                point.SetActive(true);
            }
            foreach(GameObject point in wpB)
            {
                point.SetActive(true);
            }
        }
        else
        {
            foreach(GameObject point in wpA)
            {
                point.SetActive(false);
            }
            foreach(GameObject point in wpB)
            {
                point.SetActive(true);
            }
        }
    }

    public void ValueChangeCheck()
    {
        if(slider.value==0f)
        {
            currentActive=0;
            EnableIntersection(0);
        }
        else
        {
            currentActive=1;
            EnableIntersection(1);
        }
    }
}
