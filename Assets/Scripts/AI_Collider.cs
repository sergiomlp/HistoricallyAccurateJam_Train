using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Collider : MonoBehaviour
{
    [SerializeField] private locomotive AILoco;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wagon")
        {
            if (other.transform.parent.gameObject.name != transform.parent.gameObject.name)
            {
                AILoco.speed= 0;
                AILoco.maxspeed= 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "wagon")
        {
            if (other.transform.parent.gameObject.name != transform.parent.gameObject.name)
            {
                AILoco.speed = 10;
                AILoco.maxspeed = 10;
            }
        }
    }
}
