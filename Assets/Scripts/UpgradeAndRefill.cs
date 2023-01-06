using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAndRefill : MonoBehaviour
{
    locomotive lm;

    SpeedController sp;

    ScoreCounter sc;

    GameObject gb;

    Transform spawnTransform;

    [SerializeField] GameObject train4;

    [SerializeField] GameObject scoreGameObject;

    [SerializeField] Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled=false;
        sc=scoreGameObject.GetComponent<ScoreCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="train")
        {
            canvas.enabled=true;
            Time.timeScale = 0;
            sp=other.transform.parent.gameObject.GetComponent<SpeedController>();
            gb=other.transform.parent.gameObject;
        }

    }
    public void imporveEngine()
    {
        //Debug.Log(sc);
        //Debug.Log(gb.tag);
        if(sc.money>=1000)
        {
            Debug.Log("Clicked");
            foreach(Transform child in gb.transform)
            {
                child.GetComponent<locomotive>().maxSpeed+=1;
            }
            sc.money-=1000;
        }
    }

    public void increaseCoal()
    {
        if(sc.money>=500)
        {
            //Debug.Log("Clicked");
            foreach(Transform child in gb.transform)
            {
                child.GetComponent<locomotive>().coalCount+=2;
                child.GetComponent<locomotive>().coalCountDown=60.0f;
                child.GetComponent<locomotive>().canTakeCoal=false;
            }
            sc.money-=500;
        }
    }

    public void upgradeTrain()
    {
        // spawnTransform = gb.transform;
        // Destroy(gb);
        // GameObject newTrain=Instantiate(train4,spawnTransform.position,spawnTransform.rotation);
        // newTrain.transform.position=spawnTransform.position;
        // newTrain.transform.rotation=spawnTransform.rotation;
    }

    public void continueGame()
    {
        canvas.enabled=false;
        Time.timeScale = 1;
    }
}
