using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class UI_Resource : MonoBehaviour
{
    [SerializeField] private Image requestUIPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 15f, 0);

    // Update is called once per frame
    void LateUpdate()
    {
        requestUIPrefab.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
    }
}
