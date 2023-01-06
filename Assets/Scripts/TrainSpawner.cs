using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject train;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnTrain", 1.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnTrain()
    {
        //Debug.Log();
        //Instantiate(train,train.transform.position,train.transform.rotation);
    }
}
