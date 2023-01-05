using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_FactoryRequest : MonoBehaviour
{
    [SerializeField] private Image requestUIPrefab;
    private Image uiUse;
    [SerializeField] private Vector3 offset = new Vector3(0, 15f, 0);
    [SerializeField] private Canvas HUDcanvas;

    [SerializeField] private bool IsRequesting;

    private float timer;
    private Sprite request1;
    private Text request1Value;
    private Sprite request2;
    private Text request2Value;
    private Sprite request3;
    private Text request3Value;
    private float backgroundWidth;

    void Start()
    {
        uiUse = Instantiate(requestUIPrefab, HUDcanvas.transform);
        uiUse.gameObject.SetActive(false);
        GetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsRequesting) 
        {
            uiUse.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        }
    }

    // Need receive event from request script to set the UI active

    // Set Timer/Request item and value/Background width when 1 good = 150, 2 goods = 230, 3 goods = 305

    private void GetValue()
    {
        timer = uiUse.transform.GetChild(0).GetChild(0).GetComponent<Image>().fillAmount;
        request1 = uiUse.transform.GetChild(1).GetComponent<Image>().sprite;
        request1Value = uiUse.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        request2 = uiUse.transform.GetChild(2).GetComponent<Image>().sprite;
        request2Value = uiUse.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        request3 = uiUse.transform.GetChild(2).GetComponent<Image>().sprite;
        request3Value = uiUse.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        backgroundWidth = uiUse.GetComponent<RectTransform>().sizeDelta.x;
    }
}
