using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_TrainBrakeController : MonoBehaviour
{
    [SerializeField] locomotive trainHead;
    private bool isBrake;
    public static float brakeAccelSpeed = 3f;
    private float timer = 0f;

    [SerializeField] Image brakeImage;
    [SerializeField] Sprite[] brakeSprite;

    // Update is called once per frame
    void Update()
    {
        if(isBrake) 
        {
            if (trainHead.speed != 0)
            {
                trainHead.speed = Mathf.Lerp(trainHead.speed, 0, timer / brakeAccelSpeed);
                timer += Time.deltaTime;
            }
        }
        else
        {
            if (trainHead.speed != trainHead.maxspeed)
            {
                trainHead.speed = Mathf.Lerp(trainHead.speed, trainHead.maxspeed, timer / brakeAccelSpeed);
                timer += Time.deltaTime;
            }
        }        
    }

    public void Brakecontrol()
    {
        if (!isBrake)
        {
            timer = 0;
            brakeImage.sprite = brakeSprite[0];
            Manager_Audio.GetInstance().Brake.Play();
            isBrake = true;
        }
        else
        {
            timer = 0;
            brakeImage.sprite = brakeSprite[1];
            Manager_Audio.GetInstance().trainHorn.Play();
            isBrake = false;
        }
    }
}
