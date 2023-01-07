using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CoalMeter : MonoBehaviour
{
    [SerializeField] private Image coalMeter;
    public static int coalRemain;
    public static int coalMax;
    [SerializeField] private Rigidbody trainHeadRigibody;
    public static float coalTimer;
    [SerializeField] private Text coalPercentage;

    public delegate void CoalRunsOut();
    public static event CoalRunsOut OnCoalRunsOut;

    private void Start()
    {
        coalTimer = 30f;
        coalRemain = 10;
        coalMax = 10;
    }

    void Update()
    {
        if(trainHeadRigibody.velocity.magnitude >0)
        {
            coalTimer -= Time.deltaTime;
            coalMeter.fillAmount = ((coalRemain - 1) * 30 + coalTimer) / (coalMax * 30);
            coalPercentage.text = (100*coalMeter.fillAmount).ToString("f1") + "%";
            if(coalTimer < 0)
            {
                coalTimer = 30f;
                --coalRemain;
            }
            if (coalRemain == 0)
            {
                OnCoalRunsOut();
            }
        }
    }
}
