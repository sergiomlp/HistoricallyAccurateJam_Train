using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_3WayTrackController : MonoBehaviour
{
    private bool toLeft; 
    private bool toUp;

    [SerializeField] Transform toLeftButton;
    [SerializeField] Transform toUpButton;
    [SerializeField] Transform toRightButton;

    private Image leftButtonImage;
    private Image upButtonImage;
    private Image rightButtonImage;

    [SerializeField] Sprite activeButton;
    [SerializeField] Sprite deactiveButton;

    [SerializeField] private GameObject[] toLeftWayPoint;
    [SerializeField] private GameObject[] toRightWayPoint;
    [SerializeField] private GameObject[] straightpoint;

    private void OnEnable()
    {
        UI_3WayTrainIncomingDector.trainIncoming += OnTrainIncoming;
    }

    private void OnDisable()
    {
        UI_3WayTrainIncomingDector.trainIncoming -= OnTrainIncoming;
    }

    private void Start()
    {
        leftButtonImage = toLeftButton.GetComponent<Image>();
        upButtonImage = toUpButton.GetComponent<Image>();
        rightButtonImage = toRightButton.GetComponent<Image>();
    }

    private void OnTrainIncoming(UI_3WayTrainIncomingDector.TrainIncomingDirection direction)
    {
        switch (direction)
        {
            case UI_3WayTrainIncomingDector.TrainIncomingDirection.Left:
                AdjustTrainFromLeft();
                break;
            case UI_3WayTrainIncomingDector.TrainIncomingDirection.Right:
                AdjustTrainFromRight();
                break;
            case UI_3WayTrainIncomingDector.TrainIncomingDirection.Up:
                AdjustTrainFromUp();
                break;
            default: break;
        }
    }
        
    private void AdjustTrainFromLeft()
    {
        if (toUp)
        {
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(false);
            }
        }
    }

    private void AdjustTrainFromRight()
    {
        if (toUp)
        {
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(false);
            }
        }
    }

    private void AdjustTrainFromUp()
    {
        if (toLeft)
        {
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject waypoint in toRightWayPoint)
            {
                waypoint.SetActive(true);
            }
            foreach (GameObject waypoint in toLeftWayPoint)
            {
                waypoint.SetActive(false);
            }
            foreach (GameObject waypoint in straightpoint)
            {
                waypoint.SetActive(false);
            }
        }
    }

    public void LeftRightButtonOnclick()
    {
        if(toLeft)
        {
            toLeft = false;
            leftButtonImage.sprite = deactiveButton;
            rightButtonImage.sprite = activeButton;
        }
        else
        {
            toLeft = true;
            leftButtonImage.sprite = activeButton;
            rightButtonImage.sprite = deactiveButton;
        }
    }

    public void UpButtonOnclick() 
    { 
        if(toUp)
        {
            toUp = false;
            upButtonImage.sprite = deactiveButton;
        }
        else
        {
            toUp = true;
            upButtonImage.sprite = activeButton;
        }
    }


}
