using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_3WayTrainIncomingDector : MonoBehaviour
{
    [SerializeField] private bool left;
    [SerializeField] private bool right;
    [SerializeField] private bool up;

    public enum TrainIncomingDirection
    {
        Left, Right, Up
    }

    public static event Action<TrainIncomingDirection> trainIncoming;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "train")
        {
            if (left)
            {
                trainIncoming(TrainIncomingDirection.Left);
            }
            if (right)
            {
                trainIncoming(TrainIncomingDirection.Right);
            }
            if (up)
            {
                trainIncoming(TrainIncomingDirection.Up);
            }
        }
    }
}
